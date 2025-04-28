import React, { useState } from 'react';
import Button from '../ui/Button';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setSearchBarQuery } from '../../redux/actions';

const HeroComponent = () => {
  const [searchBar, setSearchBar] = useState('');

  const navigate = useNavigate();

  const dispatch = useDispatch();

  return (
    <section
      className='relative h-[550px] bg-cover bg-center'
      style={{
        backgroundImage: `url('https://images.unsplash.com/photo-1482938289607-e9573fc25ebb?auto=format&fit=crop&w=1920&q=80')`,
      }}
    >
      <div className='absolute inset-0 bg-gradient-to-b from-black/0 to-black/60'></div>

      <div className='relative z-10 flex flex-col items-center justify-center h-full text-center text-white px-4'>
        <h1 className='text-5xl md:text-6xl font-bold leading-12 md:leading-15 mb-4'>
          Scopri Avventure
          <br />
          Indimenticabili
        </h1>
        <p className='text-lg px-10 2xl:px-0 lg:text-xl mb-8'>
          Esperienze uniche che trasformeranno il tuo modo di viaggiare
        </p>

        <div className='flex flex-col md:flex-row w-full max-w-xl gap-3 px-10 2xl:px-0'>
          <input
            type='text'
            placeholder='Cerca la tua prossima avventura…'
            className='flex-grow px-4 py-3 rounded-xl text-gray-800 focus:outline-none bg-white'
            value={searchBar}
            onChange={(e) => {
              setSearchBar(e.target.value);
            }}
          />
          <Button
            variant='secondary'
            onClick={() => {
              dispatch(setSearchBarQuery(searchBar));
              navigate('/experiences');
            }}
          >
            Cerca
          </Button>
        </div>
      </div>
    </section>
  );
};

export default HeroComponent;
