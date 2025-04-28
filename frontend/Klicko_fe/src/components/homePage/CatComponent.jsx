import React from 'react';
import Button from '../ui/Button';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';

const CatComponent = () => {
  const profile = useSelector((state) => {
    return state.profile;
  });

  return (
    <div
      className='relative h-[400px] xl:h-[300px] bg-cover bg-center'
      style={{
        backgroundImage: `url('https://images.unsplash.com/photo-1482938289607-e9573fc25ebb?auto=format&fit=crop&w=1920&q=80')`,
      }}
    >
      <div className='h-full w-full bg-primary/70'></div>
      <div className='absolute top-0 start-0 h-full w-full flex flex-col justify-center items-center px-6 md:px-0'>
        <h2 className='text-center text-5xl md:text-4xl font-bold leading-10 md:leading-16 mb-8 md:mb-4 text-white'>
          Pronto a vivere la tua prossima avventura?
        </h2>
        <p className='md:max-w-lg lg:max-w-3xl text-center text-lg md:text-xl mb-8 text-white'>
          Scopri centinaia di esperienze uniche e trasforma il tuo modo di
          viaggiare. Crea ricordi indimenticabili con le nostre avventure
          selezionate.
        </p>
        <div className='flex gap-2'>
          <Button variant='cat'>
            <Link to='/experiences'>Esplora le esperienze</Link>
          </Button>

          {profile.email === undefined && (
            <Button variant='secondary'>
              <Link to='/register'>Registrati ora</Link>
            </Button>
          )}
        </div>
      </div>
    </div>
  );
};

export default CatComponent;
