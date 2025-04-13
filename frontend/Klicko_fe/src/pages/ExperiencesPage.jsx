import { Funnel, Search, X } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import ExperienceCard from '../components/ExperienceCard';
import { useDispatch, useSelector } from 'react-redux';
import { setCategoriesList, setExperiencesList } from '../redux/actions';

const ExperiencesPage = () => {
  const [experiences, setExperiences] = useState([]);
  const [searchBar, setSearchBar] = useState('');
  const [showFilters, setShowFilters] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState('');
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(1000);

  const experiencesRedux = useSelector((state) => {
    return state.experiences;
  });

  const categoriesList = useSelector((state) => {
    return state.categories;
  });

  const dispatch = useDispatch();

  const getAllExperiences = async () => {
    try {
      const response = await fetch('https://localhost:7235/api/Experience', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();
        // console.log(data);

        dispatch(setExperiencesList(data.experiences));

        setExperiences(data.experiences);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const getAllCategories = async () => {
    try {
      const response = await fetch('https://localhost:7235/api/Category', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();
        // console.log(data);

        dispatch(setCategoriesList(data.categories));
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const searchExperiences = () => {
    const filteredExperiences = experiencesRedux.filter(
      (exp) =>
        exp.title.toLowerCase().includes(searchBar) &&
        (exp.category.name === selectedCategory ||
          exp.category.name.includes(selectedCategory)) &&
        exp.price >= minPrice &&
        exp.price <= maxPrice
    );

    console.log(filteredExperiences);
    setExperiences(filteredExperiences);
  };

  const resetFilters = () => {
    setSearchBar('');
    setSelectedCategory('');
    setMinPrice(0);
    setMaxPrice(1000);
  };

  useEffect(() => {
    if (experiencesRedux.length === 0) {
      getAllExperiences();
    } else {
      setExperiences(experiencesRedux);
    }

    if (categoriesList.length === 0) {
      getAllCategories();
    }
  }, []);

  useEffect(() => {
    // search by experience title
    searchExperiences();
  }, [searchBar, selectedCategory, minPrice, maxPrice]);

  return (
    <div className='max-w-7xl mx-auto mb-8 mt-6'>
      <div>
        <h1 className='text-3xl font-bold mb-3'>Esperienze</h1>
        <p className='text-gray-500 max-w-1/2'>
          Esplora la nostra collezione di avventure incredibili. Dalle
          esperienze adrenaliniche al relax, trova l atua prossima avventura.
        </p>
      </div>

      {/* search area */}
      <div className='bg-white p-4 rounded-2xl my-6 shadow-xs'>
        <div className=' flex justify-between items-center gap-2'>
          <div className='relative grow flex items-center me-3'>
            <input
              className='bg-background border border-gray-800/30 rounded-xl py-2 ps-10 w-full'
              placeholder='Cerca esperienze...'
              value={searchBar}
              onChange={(e) => {
                setSearchBar(e.target.value);
              }}
            />
            <Search className='absolute start-2 top-1/2 -translate-y-1/2' />
          </div>
          <Button
            variant='outline'
            icon={<Funnel className='w-3.5 h-3.5 text-gray-700' />}
            onClick={() => {
              setShowFilters(!showFilters);
            }}
          >
            Filtri
          </Button>
          <Button variant='primary' onClick={searchExperiences}>
            Cerca
          </Button>
        </div>

        {showFilters && (
          <div className='grid grid-cols-4 justify-start items-end gap-8 border-t border-gray-500/30 mt-6 pt-4'>
            <div className='flex flex-col justify-start items-start gap-2'>
              <span className='font-medium text-sm'>Categoria</span>
              <select
                name='category'
                id='categorySelect'
                className='text-sm border border-gray-500/30 bg-background rounded-lg py-1.5 px-3 w-full'
                value={selectedCategory}
                onChange={(e) => {
                  setSelectedCategory(e.target.value);
                }}
              >
                <option value=''>Tutte le categorie</option>
                {categoriesList.map((category) => (
                  <option key={category.categoryId} value={category.name}>
                    {category.name}
                  </option>
                ))}
              </select>
            </div>
            <div className='flex flex-col justify-start items-start gap-2'>
              <span className='font-medium text-sm'>Prezzo minimo</span>
              <input
                type='number'
                step={1}
                min={0}
                max={parseInt(maxPrice) - 1}
                className='text-sm border border-gray-500/30 bg-background rounded-lg py-1.5 px-3 w-full'
                value={minPrice}
                onChange={(e) => {
                  setMinPrice(e.target.value);
                }}
              />
            </div>
            <div className='flex flex-col justify-start items-start gap-2'>
              <span className='font-medium text-sm'>Prezzo massimo</span>
              <input
                type='number'
                step={1}
                min={parseInt(minPrice) + 1}
                className='text-sm border border-gray-500/30 bg-background rounded-lg py-1.5 px-3 w-full'
                value={maxPrice}
                onChange={(e) => {
                  setMaxPrice(e.target.value);
                }}
              />
            </div>
            <div>
              <Button
                variant='outline'
                icon={<X className='w-4 h-4' />}
                onClick={() => {
                  resetFilters();
                }}
                className='text-sm'
              >
                Cancella filtri
              </Button>
            </div>
          </div>
        )}
      </div>

      <div>
        <div className='flex justify-between items-center my-6'>
          <h4 className='text-xl font-semibold'>
            {experiences.length} esperienze trovate
          </h4>
          <select
            name=''
            id=''
            className='bg-background border border-gray-800/30 rounded-xl py-2 px-3'
          >
            <option value='suggested'>Consigliati</option>
            <option value='priceUp'>Prezzo: crescente</option>
            <option value='priceDown'>Prezzo: decrescente</option>
            <option value='latest'>Più recenti</option>
          </select>
        </div>
        {experiences.length > 0 && (
          <div className='grid grid-cols-4 gap-6'>
            {/* elenco esperienze */}
            {experiences.map((exp) => (
              <ExperienceCard
                key={exp.experienceId}
                experience={exp}
                className={'mb-8'}
              ></ExperienceCard>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default ExperiencesPage;
