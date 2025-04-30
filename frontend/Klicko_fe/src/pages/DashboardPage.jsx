import {
  ChevronRight,
  LayoutDashboard,
  PackageOpen,
  Settings,
  ShoppingBag,
  User,
  Users,
} from 'lucide-react';
import React from 'react';
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

  return (
    <div className='max-w-7xl mx-auto min-h-screen pt-10 px-6 xl:px-0'>
      <h1 className='text-3xl font-bold mb-3'>Dashboard</h1>
      <p className='text-gray-500 font-normal mb-5'>
        Benvenuto, {profile.name}
      </p>

      <div className='lg:grid lg:grid-cols-4 gap-6'>
        {console.log(profile)}
        <div className='hidden lg:block col-span-1 bg-white shadow rounded-xl h-fit overflow-hidden'>
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

        <div className='col-span-3 bg-white shadow rounded-xl h-fit px-6 py-8 mb-20'>
          {getContent()}
        </div>
      </div>
    </div>
  );
};

export default DashboardPage;
