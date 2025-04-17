import React, { useEffect, useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import {
  ShoppingCart,
  Menu,
  X,
  User,
  Settings,
  LogOut,
  ShoppingBag,
} from 'lucide-react';
import Button from './ui/Button';
import { Dropdown, DropdownItem, DropdownHeader } from './ui/DropdownMenu';
import { useDispatch, useSelector } from 'react-redux';
import { logoutUser, setLoggedUser, setUserCart } from '../redux/actions';
import { jwtDecode } from 'jwt-decode';

const Navbar = () => {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const profile = useSelector((state) => {
    return state.profile;
  });

  const cart = useSelector((state) => {
    return state.cart;
  });

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const manageProfile = async (accessData) => {
    const expiration = await JSON.parse(accessData).expires;

    const exp = new Date(expiration);

    if (exp - Date.now() > 0) {
      console.log('Effettuo login automatico');
      const data = await JSON.parse(accessData);
      login(data);

      getUserCart(data);
    } else {
      console.log('Effettuo logout automatico');
      logout();
    }
  };

  const login = async (data) => {
    dispatch(setLoggedUser(data));
  };

  const getUserCart = async (data) => {
    try {
      let cartId = '';
      if (data === null) {
        cartId = profile.cartId;
      } else {
        const tokenDecoded = jwtDecode(data.token);

        cartId = tokenDecoded.cartId;
      }

      const response = await fetch(
        `https://localhost:7235/api/Cart/GetCart/${cartId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();

        // console.log(data.cart);

        dispatch(setUserCart(data.cart));
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const logout = () => {
    dispatch(logoutUser());
  };

  const toggleMobileMenu = () => setMobileMenuOpen(!mobileMenuOpen);

  useEffect(() => {
    const accessData = localStorage.getItem('klicko_token');

    if (accessData !== null && !profile?.email) {
      manageProfile(accessData);
    }
  }, [profile]);

  useEffect(() => {
    if (cart.modified === true) {
      getUserCart(null);
    }
  }, [cart]);

  return (
    <header className='bg-white shadow-sm sticky top-0 z-40'>
      <div className='max-w-7xl mx-auto px-4 py-3 flex justify-between items-center'>
        <Link to='/' className='flex items-center space-x-2'>
          <span className='font-serif text-2xl font-bold bg-clip-text text-transparent bg-gradient-to-r from-blue-500 to-blue-700'>
            Klicko
          </span>
        </Link>

        <nav className='hidden md:flex items-center space-x-6'>
          <Link
            to='/experiences'
            className='font-medium hover:text-primary transition-colors'
          >
            Esperienze
          </Link>
          <Link
            to='/categories'
            className='font-medium hover:text-primary transition-colors'
          >
            Categorie
          </Link>
          <Link
            to='/about'
            className='font-medium hover:text-primary transition-colors'
          >
            Chi siamo
          </Link>
        </nav>

        <div className='flex items-center space-x-4'>
          <Link to='/cart' className='relative flex items-center gap-1'>
            {cart.experiences != undefined && cart.experiences.length > 0 && (
              <span className='text-lg font-semibold'>
                {cart.experiences.length}
              </span>
            )}
            <ShoppingCart className='h-6 w-6' />
          </Link>

          {profile?.email ? (
            // dropdown opzioni profilo
            <Dropdown
              trigger={
                <Button variant='icon' size='icon'>
                  <User className='h-5 w-5' />
                </Button>
              }
              align='right'
            >
              <DropdownHeader>Il tuo account</DropdownHeader>
              <p className='text-gray-500 text-xs px-4 mb-2'>{profile.email}</p>
              <DropdownItem>
                <Link
                  to='/dashboard'
                  className='w-full flex items-center justify-start gap-2'
                >
                  <User size={16} />
                  Dashbord
                </Link>
              </DropdownItem>
              <DropdownItem>
                <Link
                  to='/dashboard/orders'
                  className='w-full flex items-center justify-start gap-2'
                >
                  <ShoppingBag size={16} />I miei ordini
                </Link>
              </DropdownItem>
              <DropdownItem>
                <Link
                  to='/dashboard/settings'
                  className='w-full flex items-center justify-start gap-2'
                >
                  <Settings size={16} />
                  Impostazioni
                </Link>
              </DropdownItem>
              <DropdownItem divider />
              <DropdownItem icon={<LogOut size={16} />} onClick={logout} danger>
                Disconnetti
              </DropdownItem>
            </Dropdown>
          ) : (
            <div className='hidden md:flex items-center space-x-2'>
              <Button variant='outline' size='md'>
                <Link to='/login'>Accedi</Link>
              </Button>
              <Button variant='primary'>
                <Link to='/register'>Registrati</Link>
              </Button>
            </div>
          )}

          <button className='md:hidden' onClick={toggleMobileMenu}>
            {mobileMenuOpen ? (
              <X className='h-6 w-6' />
            ) : (
              <Menu className='h-6 w-6' />
            )}
          </button>
        </div>
      </div>

      {mobileMenuOpen && (
        <div className='md:hidden bg-white border-t'>
          <div className='container mx-auto py-4 space-y-3'>
            <Link
              to='/experiences'
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Esperienze
            </Link>
            <Link
              to='/categories'
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Categorie
            </Link>
            <Link
              to='/about'
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Chi siamo
            </Link>

            <div className='flex flex-col space-y-2 pt-3 border-t'>
              <Button
                variant='outline'
                size='md'
                className='mx-4'
                onClick={() => {
                  navigate('/login');
                }}
              >
                Accedi
              </Button>
              <Button
                variant='primary'
                className='mx-4'
                onClick={() => {
                  navigate('/register');
                }}
              >
                Registrati
              </Button>
            </div>
          </div>
        </div>
      )}
    </header>
  );
};

export default Navbar;
