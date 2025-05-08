import React from 'react';

const MainComponent = () => {
  return (
    <div className='flex flex-col justify-center items-center w-full min-h-[50vh]'>
      <h2 className='mb-2 text-2xl font-semibold'>
        Benvenuto nella tua area personale
      </h2>
      <p className='font-normal text-gray-500'>
        Naviga tra le voci del menu per visualizzare la tua scelta
      </p>
    </div>
  );
};

export default MainComponent;
