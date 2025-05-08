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
        backgroundImage: `url('/assets/images/heroBackground.jpg')`,
      }}
    >
      <div className='w-full h-full bg-primary/70'></div>
      <div className='absolute top-0 flex flex-col items-center justify-center w-full h-full px-6 start-0 md:px-0'>
        <h2 className='mb-8 text-3xl font-bold leading-10 text-center text-white xs:text-5xl md:text-4xl md:leading-16 md:mb-4'>
          Pronto a vivere la tua prossima avventura?
        </h2>
        <p className='mb-8 text-base text-center text-white md:max-w-lg lg:max-w-3xl xs:text-lg md:text-xl'>
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
