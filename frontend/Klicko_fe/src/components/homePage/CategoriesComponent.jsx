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
    <div className='text-center bg-[#F1F5F9] py-15'>
      <p className='text-primary font-semibold'>Categorie</p>

      <h2 className=' text-4xl font-bold'>Esplora per categoria</h2>

      <p className='text-gray-500 my-8'>
        Scopri la nostra selezione di esperienze uniche suddivise per categoria
      </p>

      <div className='columns-4 gap-10 max-w-7xl mx-auto'>
        {categories.map((category) => (
          <div
            key={category.id}
            className='text-center bg-white py-6 px-12 rounded-2xl shadow-sm hover:shadow-xl hover:-translate-y-3 duration-700 ease-in-out cursor-pointer'
            onClick={() => {
              // setSelectedCategory(category.name);
              dispatch(setSelectedCategoryName(category.name));
              navigate('/experiences');
            }}
          >
            {category.icon}
            <h4 className='text-lg font-semibold my-3'>{category.name}</h4>
            <p className='text-sm text-gray-500'>{category.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CategoriesComponent;
