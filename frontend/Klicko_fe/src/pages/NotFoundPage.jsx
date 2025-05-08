import React from 'react';
import Button from '../components/ui/Button';
import { useNavigate } from 'react-router-dom';

const NotFoundPage = () => {
  const navigate = useNavigate();

  return (
    <div className='max-w-7xl mx-auto mt-6 flex flex-col justify-center items-center gap-5 min-h-[80vh] px-12 xl:px-0'>
      <h1 className='font-bold text-9xl text-primary'>404</h1>
      <h2 className='text-3xl font-semibold'>Pagina non trovata</h2>
      <p className='max-w-xl text-center text-gray-500'>
        La pagina che stai cercando non esiste o è stata spostata. Torna alla
        homepage per continuare a esplorare le nostre avventure.
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

export default NotFoundPage;
