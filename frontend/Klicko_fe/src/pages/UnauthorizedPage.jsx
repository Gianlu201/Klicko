import React from 'react';
import { useNavigate } from 'react-router-dom';
import Button from '../components/ui/Button';

const UnauthorizedPage = () => {
  const navigate = useNavigate();

  return (
    <div className='max-w-7xl mx-auto flex flex-col justify-center items-center gap-5 min-h-[100vh] px-12 xl:px-0 -translate-y-20'>
      <h1 className='text-4xl font-bold md:text-7xl text-primary'>
        Area riservata
      </h1>
      <p className='max-w-xl text-sm text-center text-gray-500 md:text-base'>
        Il tuo profilo non è autorizzato ad accedere a quest'area!
      </p>
      <div className='flex items-center justify-center gap-6'>
        <Button
          variant='primary'
          onClick={() => {
            navigate('/');
          }}
        >
          Torna alla homepage
        </Button>
        <Button
          variant='outline'
          onClick={() => {
            navigate('/experiences');
          }}
        >
          Esplora esperienze
        </Button>
      </div>
    </div>
  );
};

export default UnauthorizedPage;
