import React from 'react';
import Button from '../../ui/Button';

const HighlightedComponent = () => {
  return (
    <div className='max-w-7xl mx-auto mt-16'>
      <p>Esperienze in evidenza</p>
      <div className='flex justify-between items-center'>
        <h2 className=' text-4xl font-bold'>Le nostre migliori avventure</h2>
        <Button variant='outline' size='md'>
          Vedi tutte
        </Button>
      </div>
    </div>
  );
};

export default HighlightedComponent;
