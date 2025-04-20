import { User } from 'lucide-react';
import React from 'react';
import { useSelector } from 'react-redux';

const ProfileComponent = () => {
  const profile = useSelector((state) => {
    return state.profile;
  });

  return (
    <>
      <h2 className='text-2xl font-bold flex justify-start items-center gap-2 mb-10'>
        <User className='w-6 h-6' />
        Informazioni account
      </h2>

      <div className='my-2'>
        <div className='mb-6'>
          <p className='text-gray-500 font-medium text-sm mb-1'>Nome</p>
          <p>
            {profile.name} {profile.surname}
          </p>
        </div>

        <div className='mb-6'>
          <p className='text-gray-500 font-medium text-sm mb-1'>Email</p>
          <p>{profile.email}</p>
        </div>

        <div>
          <p className='text-gray-500 font-medium text-sm mb-1'>
            Tipologia profilo
          </p>
          <p>{profile.role}</p>
        </div>
      </div>
    </>
  );
};

export default ProfileComponent;
