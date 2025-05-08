import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {
  ShoppingCart,
  Menu,
  X,
  User,
  Settings,
  LogOut,
  ShoppingBag,
  BadgePercent,
  Tickets,
  PackageOpen,
  Users,
  LayoutDashboard,
} from 'lucide-react';
import Button from './ui/Button';
import { Dropdown, DropdownItem, DropdownHeader } from './ui/DropdownMenu';
import { useDispatch, useSelector } from 'react-redux';
import {
  logoutUser,
  setCartFromLocal,
  setLoggedUser,
  setUserCart,
  setUserFidelityCard,
} from '../redux/actions';
import { jwtDecode } from 'jwt-decode';
import { toast } from 'sonner';

const Navbar = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isFirstLoad, setIsFirstLoad] = useState(true);
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const profile = useSelector((state) => {
    return state.profile;
  });

  const cart = useSelector((state) => {
    return state.cart;
  });

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const dropDownOptions = [
    {
      id: 1,
      option: 'Dashboard',
      style: 'hidden lg:flex',
      icon: <User size={16} />,
      url: '/dashboard',
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 2,
      option: 'Profilo',
      style: 'block lg:hidden',
      icon: <User size={16} />,
      url: '/dashboard/profile',
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 3,
      option: 'Gestisci esperienze',
      style: 'block lg:hidden',
      icon: <PackageOpen size={16} />,
      url: '/dashboard/experiences',
      authorizedRoles: 'Admin, Seller',
    },
    {
      id: 4,
      option: 'I miei ordini',
      style: '',
      icon: <ShoppingBag size={16} />,
      url: '/dashboard/orders',
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 5,
      option: 'Vouchers',
      style: '',
      icon: <Tickets size={16} />,
      url: '/redeemVoucher',
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 6,
      option: 'Coupon',
      style: '',
      icon: <BadgePercent size={16} />,
      url: '/coupons',
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 7,
      option: 'Dashboard admin',
      style: 'block lg:hidden',
      icon: <LayoutDashboard size={16} />,
      url: '/dashboard/admin',
      authorizedRoles: 'Admin',
    },
    {
      id: 8,
      option: 'Gestione utenti',
      style: 'block lg:hidden',
      icon: <Users size={16} />,
      url: '/dashboard/users',
      authorizedRoles: 'Admin',
    },
    {
      id: 9,
      option: 'Impostazioni',
      style: '',
      icon: <Settings size={16} />,
      url: '/dashboard/settings',
      authorizedRoles: 'Admin, Seller, User',
    },
  ];

  const manageProfile = async (accessData) => {
    const expiration = await JSON.parse(accessData).expires;

    const exp = new Date(expiration);

    if (exp - Date.now() > 0) {
      const data = await JSON.parse(accessData);
      login(data);

      getUserCart(data);
      getUserFidelityCard(data);
    } else {
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

      if (cart.cartId === '' && cart.experiences.length > 0) {
        await sendExperiencesFromLocalCart(cartId);
      }

      const response = await fetch(`${backendUrl}/Cart/GetCart/${cartId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        dispatch(setUserCart(data.cart));
      } else {
        throw new Error('Errore nel recupero del carrello!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const sendExperiencesFromLocalCart = async (cartId) => {
    try {
      let body = [];
      cart.experiences.forEach((exp) => {
        body.push({
          experienceId: exp.experienceId,
          quantity: exp.quantity,
        });
      });

      const response = await fetch(
        `${backendUrl}/Cart/AddExperienceFromLocalCart/${cartId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(body),
        }
      );
      if (response.ok) {
        localStorage.removeItem('klickoLocalCart');
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const getUserFidelityCard = async (data) => {
    try {
      const tokenDecoded = jwtDecode(data.token);

      const response = await fetch(
        `${backendUrl}/FidelityCard/getFidelityCardById/${tokenDecoded.fidelityCardId}`,
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();

        dispatch(setUserFidelityCard(data.fidelityCard));
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const logout = () => {
    dispatch(logoutUser());
    navigate('/');
  };

  const updateLocalStorageCart = () => {
    if (isFirstLoad) {
      const localCart = localStorage.getItem('klickoLocalCart');
      if (localCart !== null) {
        dispatch(setCartFromLocal(JSON.parse(localCart)));
      }
      setIsFirstLoad(false);
    } else {
      localStorage.removeItem('klickoLocalCart');

      localStorage.setItem('klickoLocalCart', JSON.stringify(cart));
    }
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

    if (cart.cartId === '') {
      updateLocalStorageCart();
    }
  }, [cart]);

  return (
    <header className='sticky top-0 z-40 bg-white shadow-sm'>
      <div className='flex items-center justify-between px-4 py-3 mx-auto max-w-7xl'>
        <Link
          to='/'
          className='flex items-center space-x-2'
          onClick={() => {
            setMobileMenuOpen(false);
          }}
        >
          <span
            translate='no'
            className='notranslate font-serif text-2xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-blue-500 to-blue-700'
          >
            Klicko
          </span>
        </Link>

        <nav className='items-center hidden space-x-6 md:flex'>
          <Link
            to='/experiences'
            className='font-medium transition-colors hover:text-primary'
          >
            Esperienze
          </Link>
          <Link
            to='/categories'
            className='font-medium transition-colors hover:text-primary'
          >
            Categorie
          </Link>
          <Link
            to='/about'
            className='font-medium transition-colors hover:text-primary'
          >
            Chi siamo
          </Link>

          {profile?.email && (
            <Link
              to='/loyalty'
              className='font-medium transition-colors hover:text-primary'
            >
              Programma fedeltà
            </Link>
          )}
        </nav>

        <div className='flex items-center space-x-4'>
          <Link
            to='/cart'
            className='relative flex items-center gap-1'
            onClick={() => {
              setMobileMenuOpen(false);
            }}
          >
            {cart.experiences != undefined && cart.experiences.length > 0 && (
              <span className='absolute flex items-center justify-center w-5 h-5 text-xs font-semibold text-white rounded-full -top-2 -end-2 bg-secondary'>
                {cart.experiences.length}
              </span>
            )}
            <ShoppingCart className='w-full h-full' />
          </Link>

          {profile?.email ? (
            <Dropdown
              trigger={
                <Button
                  variant='icon'
                  size='icon'
                  onClick={() => {
                    setMobileMenuOpen(false);
                  }}
                >
                  {/* <User className='w-5 h-5' /> */}
                  <span className='font-bold'>
                    {profile?.name[0]}
                    {profile?.surname[0]}
                  </span>
                </Button>
              }
              align='right'
            >
              <DropdownHeader>Il tuo account</DropdownHeader>
              <p className='px-4 mb-2 text-xs text-gray-500'>{profile.email}</p>

              {dropDownOptions.map(
                (option) =>
                  option.authorizedRoles.includes(profile.role) && (
                    <DropdownItem
                      key={option.id}
                      className={option.style}
                      onClick={() => {
                        navigate(option.url);
                      }}
                      icon={option.icon}
                    >
                      {option.option}
                    </DropdownItem>
                  )
              )}

              <DropdownItem divider />
              <DropdownItem icon={<LogOut size={16} />} onClick={logout} danger>
                Disconnetti
              </DropdownItem>
            </Dropdown>
          ) : (
            <div className='items-center hidden space-x-2 md:flex'>
              <Button
                variant='outline'
                size='md'
                onClick={() => {
                  navigate('login');
                }}
              >
                Accedi
              </Button>
              <Button
                variant='primary'
                onClick={() => {
                  navigate('/register');
                }}
              >
                Registrati
              </Button>
            </div>
          )}

          <button className='md:hidden' onClick={toggleMobileMenu}>
            {mobileMenuOpen ? (
              <X className='w-6 h-6 cursor-pointer' />
            ) : (
              <Menu className='w-6 h-6 cursor-pointer' />
            )}
          </button>
        </div>
      </div>

      {mobileMenuOpen && (
        <div className='bg-white border-t shadow-2xl md:hidden'>
          <div className='container py-4 mx-auto space-y-3 shadow-2xl'>
            <Link
              to='/experiences'
              className='block px-4 py-2 rounded-md hover:bg-muted hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Esperienze
            </Link>
            <Link
              to='/categories'
              className='block px-4 py-2 rounded-md hover:bg-muted hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Categorie
            </Link>
            <Link
              to='/about'
              className='block px-4 py-2 rounded-md hover:bg-muted hover:text-primary'
              onClick={toggleMobileMenu}
            >
              Chi siamo
            </Link>
            {profile?.email && (
              <Link
                to='/loyalty'
                className='block px-4 py-2 rounded-md hover:bg-muted hover:text-primary'
                onClick={toggleMobileMenu}
              >
                Programma fedeltà
              </Link>
            )}

            {!profile?.email && (
              <div className='flex flex-col pt-3 space-y-2 border-t'>
                <Button
                  variant='outline'
                  size='md'
                  className='mx-4'
                  onClick={() => {
                    navigate('/login');
                    toggleMobileMenu();
                  }}
                >
                  Accedi
                </Button>
                <Button
                  variant='primary'
                  className='mx-4'
                  onClick={() => {
                    navigate('/register');
                    toggleMobileMenu();
                  }}
                >
                  Registrati
                </Button>
              </div>
            )}
          </div>
        </div>
      )}
    </header>
  );
};

export default Navbar;
