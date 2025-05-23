import {
  ChevronRight,
  LayoutDashboard,
  PackageOpen,
  Settings,
  ShoppingBag,
  User,
  Users,
} from 'lucide-react';
import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { Link, useNavigate, useParams } from 'react-router-dom';
import MainComponent from '../components/dashboardPage/MainComponent';
import OrdersComponent from '../components/dashboardPage/OrdersComponent';
import ExperiencesComponent from '../components/dashboardPage/ExperiencesComponent';
import ExperienceForm from '../components/dashboardPage/ExperienceForm';
import DashboardAdmin from '../components/dashboardPage/DashboardAdmin';
import DashboardUsers from '../components/dashboardPage/DashboardUsers';
import ProfileComponent from '../components/dashboardPage/ProfileComponent';
import SettingsComponent from '../components/dashboardPage/SettingsComponent';

const DashboardPage = () => {
  const profile = useSelector((state) => {
    return state.profile;
  });

  const params = useParams();

  const navigate = useNavigate();

  const options = [
    {
      id: 1,
      url: '/dashboard/orders',
      title: 'I miei ordini',
      partialUrl: 'orders',
      icon: <ShoppingBag className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 2,
      url: '/dashboard/profile',
      title: 'Profilo',
      partialUrl: 'profile',
      icon: <User className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin, Seller, User',
    },
    {
      id: 3,
      url: '/dashboard/experiences',
      title: 'Esperienze',
      partialUrl: 'experiences',
      icon: <PackageOpen className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin, Seller',
    },
    {
      id: 4,
      url: '/dashboard/admin',
      title: 'Dashboard admin',
      partialUrl: 'admin',
      icon: <LayoutDashboard className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin',
    },
    {
      id: 5,
      url: '/dashboard/users',
      title: 'Gestione utenti',
      partialUrl: 'users',
      icon: <Users className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin',
    },
    {
      id: 6,
      url: '/dashboard/settings',
      title: 'Impostazioni',
      partialUrl: 'settings',
      icon: <Settings className='w-4.5 h-4.5' />,
      authorizedRoles: 'Admin, Seller, User',
    },
  ];

  const checkAuthentication = () => {
    let tokenObj = localStorage.getItem('klicko_token');

    if (!tokenObj) {
      navigate('/login');
    }
  };

  const getContent = () => {
    switch (params.tab) {
      case undefined:
        return <MainComponent />;

      case 'orders':
        return <OrdersComponent />;

      case 'profile':
        return <ProfileComponent />;

      case 'experiences':
        if (params.option === 'add' || params.option === 'edit') {
          return <ExperienceForm />;
        }
        return <ExperiencesComponent />;

      case 'admin':
        return <DashboardAdmin />;

      case 'users':
        return <DashboardUsers />;

      case 'settings':
        return <SettingsComponent />;

      default:
        navigate('/404');
        break;
    }
  };

  useEffect(() => {
    checkAuthentication();
  }, []);

  return (
    <div className='min-h-screen px-6 pt-10 mx-auto max-w-7xl xl:px-0'>
      <h1 className='mb-3 text-3xl font-bold'>Dashboard</h1>
      <p className='mb-5 font-normal text-gray-500'>
        Benvenuto, {profile.name}
      </p>

      <div className='gap-6 lg:grid lg:grid-cols-4'>
        <div className='hidden col-span-1 overflow-hidden bg-white shadow lg:block rounded-xl h-fit'>
          <ul>
            {options.map(
              (opt) =>
                opt.authorizedRoles.includes(profile.role) && (
                  <li key={opt.id}>
                    <Link
                      to={opt.url}
                      className={`flex justify-between items-center border-b border-gray-400/30 cursor-pointer px-5 py-4 ${
                        params.tab === opt.partialUrl
                          ? 'bg-primary/15 text-primary font-medium'
                          : 'hover:bg-gray-100'
                      } ${opt.id === options.length ? 'border-0' : ''}`}
                    >
                      <span className='flex items-center gap-2'>
                        {opt.icon}
                        {opt.title}
                      </span>
                      <ChevronRight className='w-4.5 h-4.5' />
                    </Link>
                  </li>
                )
            )}
          </ul>
        </div>

        <div className='col-span-3 px-6 py-8 mb-20 bg-white shadow rounded-xl h-fit'>
          {getContent()}
        </div>
      </div>
    </div>
  );
};

export default DashboardPage;
