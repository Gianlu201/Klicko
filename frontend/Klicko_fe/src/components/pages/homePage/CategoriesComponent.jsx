import { Compass, Droplet, Trophy, Wind } from 'lucide-react';
import React from 'react';

const CategoriesComponent = () => {
  const categories = [
    {
      id: 0,
      name: 'Aria',
      description: 'Sorvola paesaggi mozzafiato con mongolfiere e parapendio',
    },
    {
      id: 1,
      name: 'Acqua',
      description: 'Avventure emozionanti su fiumi, laghi e mari',
    },
    {
      id: 2,
      name: 'Terra',
      description: 'Escursioni e avventure in luoghi spettacolari',
    },
    {
      id: 3,
      name: 'Motori',
      description: 'Emozioni ad alta velocità con auto da sogno',
    },
  ];

  const icons = [
    {
      icon: <Wind className='inline-block text-[#F97415]' />,
    },
    {
      icon: <Droplet className='inline-block text-[#19aeff]' />,
    },
    {
      icon: <Compass className='inline-block text-green-500' />,
    },
    {
      icon: <Trophy className='inline-block text-red-700' />,
    },
  ];

  return (
    <div className='text-center bg-[#F1F5F9] py-15'>
      <p className='text-[#19aeff] font-semibold'>Categorie</p>

      <h2 className=' text-4xl font-bold'>Esplora per categoria</h2>

      <p className='text-gray-500 my-5'>
        Scopri la nostra selezione di esperienze uniche suddivise per categoria
      </p>

      <div className='columns-4 gap-10 max-w-7xl mx-auto'>
        {categories.map((category) => (
          <div
            key={category.id}
            className='text-center bg-white py-6 px-12 rounded-2xl'
          >
            {icons[category.id].icon}
            <h4 className='text-lg font-semibold my-3'>{category.name}</h4>
            <p className='text-sm text-gray-500'>{category.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CategoriesComponent;
