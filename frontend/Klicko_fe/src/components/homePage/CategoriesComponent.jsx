import { Compass, Droplet, Trophy, Wind } from 'lucide-react';
import React from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { setSelectedCategoryName } from '../../redux/actions';

const CategoriesComponent = () => {
  const navigate = useNavigate();

  const dispatch = useDispatch();

  const categories = [
    {
      id: 0,
      name: 'Aria',
      description: 'Sorvola paesaggi mozzafiato con mongolfiere e parapendio',
      icon: <Wind className='inline-block text-secondary' />,
    },
    {
      id: 1,
      name: 'Acqua',
      description: 'Avventure emozionanti su fiumi, laghi e mari',
      icon: <Droplet className='inline-block text-primary' />,
    },
    {
      id: 2,
      name: 'Trekking',
      description: 'Escursioni e avventure in luoghi spettacolari',
      icon: <Compass className='inline-block text-green-600' />,
    },
    {
      id: 3,
      name: 'Motori',
      description: 'Emozioni ad alta velocità con auto da sogno',
      icon: <Trophy className='inline-block text-red-700' />,
    },
  ];

  return (
    <div className='text-center bg-[#F1F5F9] py-15 px-6 md-px-0'>
      <p className='font-semibold text-primary'>Categorie</p>

      <h2 className='text-2xl font-bold xs:text-4xl'>Esplora per categoria</h2>

      <p className='my-8 text-gray-500'>
        Scopri la nostra selezione di esperienze uniche suddivise per categoria
      </p>

      <div className='grid grid-cols-2 gap-10 mx-auto md:grid-cols-4 max-w-7xl'>
        {categories.map((category) => (
          <div
            key={category.id}
            className='px-2 py-6 text-xs text-center duration-700 ease-in-out bg-white shadow-sm cursor-pointer xs:text-base xs:px-10 md:px-8 lg:px-12 rounded-2xl hover:shadow-xl hover:-translate-y-3'
            onClick={() => {
              dispatch(setSelectedCategoryName(category.name));
              navigate('/experiences');
            }}
          >
            {category.icon}
            <h4 className='my-3 text-lg font-semibold'>{category.name}</h4>
            <p className='text-sm text-gray-500'>{category.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CategoriesComponent;
