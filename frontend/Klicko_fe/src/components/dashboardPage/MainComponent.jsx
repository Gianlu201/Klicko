import React from 'react';

const MainComponent = () => {
  return (
    <div className='flex flex-col justify-center items-center w-full min-h-[50vh]'>
      <h2 className='text-2xl font-semibold mb-2'>
        Benvenuto nella tua area personale
      </h2>
      <p className='text-gray-500 font-normal'>
        Naviga tra le voci del menu per visualizzare la tua scelta
      </p>
    </div>
  );
};

export default MainComponent;
