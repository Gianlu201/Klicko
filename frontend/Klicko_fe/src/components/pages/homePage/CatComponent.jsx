import React from 'react';
import Button from '../../ui/Button';
import { Link } from 'react-router-dom';

const CatComponent = () => {
  return (
    <div
      className='relative h-[300px] bg-cover bg-center'
      style={{
        backgroundImage: `url('https://images.unsplash.com/photo-1482938289607-e9573fc25ebb?auto=format&fit=crop&w=1920&q=80')`,
      }}
    >
      <div className='h-full w-full bg-primary/70'></div>
      <div className='absolute top-0 start-0 h-full w-full flex flex-col justify-center items-center'>
        <h2 className='text-4xl md:text-4xl font-bold  leading-15 mb-4 text-white'>
          Pronto a vivere la tua prossima avventura?
        </h2>
        <p className='max-w-3xl text-center text-lg md:text-xl mb-8 text-white'>
          Scopri centinaia di esperienze uniche e trasforma il tuo modo di
          viaggiare. Crea ricordi indimenticabili con le nostre avventure
          selezionate.
        </p>
        <div className='flex gap-2'>
          <Button variant='cat'>
            <Link to='/experiences'>Esplora le esperienze</Link>
          </Button>
          <Button variant='secondary'>Registrati ora</Button>
        </div>
      </div>
    </div>
  );
};

export default CatComponent;
