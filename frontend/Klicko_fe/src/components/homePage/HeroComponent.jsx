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
        backgroundImage: `url('/assets/images/heroBackground.jpg')`,
      }}
    >
      <div className='absolute inset-0 bg-gradient-to-b from-black/0 to-black/60'></div>

      <div className='relative z-10 flex flex-col items-center justify-center h-full px-4 text-center text-white'>
        <h1 className='mb-4 text-4xl font-bold xs:text-5xl md:text-6xl leading-12 md:leading-15'>
          Scopri Avventure
          <br />
          Indimenticabili
        </h1>
        <p className='px-10 mb-8 text-lg 2xl:px-0 lg:text-xl'>
          Esperienze uniche che trasformeranno il tuo modo di viaggiare
        </p>

        <form
          className='flex flex-col w-full max-w-xl gap-3 px-10 md:flex-row 2xl:px-0'
          onSubmit={(e) => {
            e.preventDefault();
            dispatch(setSearchBarQuery(searchBar));
            navigate('/experiences');
          }}
        >
          <input
            type='text'
            placeholder='Cerca la tua prossima avventura…'
            className='flex-grow px-4 py-3 text-xs text-gray-800 bg-white rounded-xl focus:outline-none xs:text-sm md:text-base'
            value={searchBar}
            onChange={(e) => {
              setSearchBar(e.target.value.toLowerCase());
            }}
          />
          <Button variant='secondary' type='submit'>
            Cerca
          </Button>
        </form>
      </div>
    </section>
  );
};

export default HeroComponent;
