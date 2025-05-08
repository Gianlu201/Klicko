import { User } from 'lucide-react';
import React from 'react';
import { useSelector } from 'react-redux';

const ProfileComponent = () => {
  const profile = useSelector((state) => {
    return state.profile;
  });

  return (
    <>
      <h2 className='flex items-center justify-start gap-2 mb-10 text-2xl font-bold'>
        <User className='w-6 h-6' />
        Informazioni account
      </h2>

      <div className='my-2'>
        <div className='mb-6'>
          <p className='mb-1 text-sm font-medium text-gray-500'>Nome</p>
          <p>
            {profile.name} {profile.surname}
          </p>
        </div>

        <div className='mb-6'>
          <p className='mb-1 text-sm font-medium text-gray-500'>Email</p>
          <p>{profile.email}</p>
        </div>

        <div>
          <p className='mb-1 text-sm font-medium text-gray-500'>
            Tipologia profilo
          </p>
          <p>{profile.role}</p>
        </div>
      </div>
    </>
  );
};

export default ProfileComponent;
