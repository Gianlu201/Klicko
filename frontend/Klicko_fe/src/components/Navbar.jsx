import React, { useState } from 'react';
import { Link } from 'react-router-dom';
// import { useAuthStore, useCartStore } from '@/lib/store';
// import { Button } from '@/components/ui/button';
import { ShoppingCart, Menu, X, User, Settings, LogOut } from 'lucide-react';
import Button from './ui/Button';
import { Dropdown, DropdownItem, DropdownHeader } from './ui/DropdownMenu';

const Navbar = () => {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  // const { isAuthenticated, user, logout } = useAuthStore();
  // const { totalItems } = useCartStore();

  const toggleMobileMenu = () => setMobileMenuOpen(!mobileMenuOpen);

  // const getProfileMenuItems = () => {
  //   if (!user) return [];

  //   const items = [
  //     { label: 'Il mio profilo', href: '/profile' },
  //     { label: 'I miei ordini', href: '/dashboard/orders' },
  //   ];

  //   if (user.role === 'seller' || user.role === 'admin') {
  //     items.push({ label: 'Le mie esperienze', href: '/dashboard/experiences' });
  //   }

  //   if (user.role === 'admin') {
  //     items.push({ label: 'Gestione utenti', href: '/dashboard/users' });
  //   }

  //   return items;
  // };

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
            className='font-medium hover:text-[#19AEFF] transition-colors'
          >
            Esperienze
          </Link>
          <Link
            to='/categories'
            className='font-medium hover:text-[#19AEFF] transition-colors'
          >
            Categorie
          </Link>
          <Link
            to='/about'
            className='font-medium hover:text-[#19AEFF] transition-colors'
          >
            Chi siamo
          </Link>
        </nav>

        <div className='flex items-center space-x-4'>
          <Link to='/cart' className='relative'>
            <ShoppingCart className='h-6 w-6' />
            {/* {totalItems > 0 && (
              <span className="absolute -top-2 -right-2 bg-secondary text-white rounded-full w-5 h-5 flex items-center justify-center text-xs">
                {totalItems}
              </span>
            )} */}
          </Link>

          {/* {isAuthenticated ? (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon" className="rounded-full">
                  <User className="h-5 w-5" />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end" className="w-56">
                <DropdownMenuLabel>
                  {user?.name}
                  <p className="text-xs text-muted-foreground mt-1">{user?.email}</p>
                </DropdownMenuLabel>
                <DropdownMenuSeparator />
                {getProfileMenuItems().map((item, index) => (
                  <DropdownMenuItem key={index} asChild>
                    <Link to={item.href}>{item.label}</Link>
                  </DropdownMenuItem>
                ))}
                <DropdownMenuSeparator />
                <DropdownMenuItem onClick={logout} className="text-destructive">
                  Logout
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          ) : ( */}

          <Dropdown
            trigger={
              <Button variant='outline' size='icon' className='rounded-full'>
                <User className='h-5 w-5' />
              </Button>
            }
            align='right'
          >
            <DropdownHeader>Il tuo account</DropdownHeader>
            <DropdownItem
              icon={<User size={16} />}
              onClick={() => console.log('Profilo')}
            >
              Profilo
            </DropdownItem>
            <DropdownItem
              icon={<Settings size={16} />}
              onClick={() => console.log('Impostazioni')}
            >
              Impostazioni
            </DropdownItem>
            <DropdownItem divider />
            <DropdownItem
              icon={<LogOut size={16} />}
              onClick={() => console.log('Logout')}
              danger
            >
              Disconnetti
            </DropdownItem>
          </Dropdown>

          <div className='hidden md:flex items-center space-x-2'>
            <Button variant='outline' size='md'>
              <Link to='/login'>Accedi</Link>
            </Button>
            <Button variant='primary'>
              <Link to='/register'>Registrati</Link>
            </Button>
          </div>
          {/* )} */}

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
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-[#19AEFF]'
              onClick={toggleMobileMenu}
            >
              Esperienze
            </Link>
            <Link
              to='/categories'
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-[#19AEFF]'
              onClick={toggleMobileMenu}
            >
              Categorie
            </Link>
            <Link
              to='/about'
              className='block px-4 py-2 hover:bg-muted rounded-md hover:text-[#19AEFF]'
              onClick={toggleMobileMenu}
            >
              Chi siamo
            </Link>

            {/* {!isAuthenticated && (
              <div className="flex flex-col space-y-2 pt-3 border-t">
                <Button variant="outline" asChild>
                  <Link to="/login" onClick={toggleMobileMenu}>Accedi</Link>
                </Button>
                <Button asChild>
                  <Link to="/register" onClick={toggleMobileMenu}>Registrati</Link>
                </Button>
              </div>
            )} */}

            <div className='flex flex-col space-y-2 pt-3 border-t'>
              <Button variant='outline' size='md' className='mx-4'>
                <Link to='/login'>Accedi</Link>
              </Button>
              <Button variant='primary' className='mx-4'>
                <Link to='/register'>Registrati</Link>
              </Button>
            </div>
          </div>
        </div>
      )}
    </header>
  );
};

export default Navbar;
