import { Funnel, Search, X } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../components/ui/Button';
import ExperienceCard from '../components/ExperienceCard';
import { useDispatch, useSelector } from 'react-redux';
import { setSelectedCategoryName, setSearchBarQuery } from '../redux/actions';
import { toast } from 'sonner';
import ExperiencesSkeletonLoader from '../components/ui/ExperiencesSkeletonLoader';

const ExperiencesPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [experiences, setExperiences] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filteredExperiences, setFilteredExperiences] = useState([]);
  const [showFilters, setShowFilters] = useState(false);
  const [searchBar, setSearchBar] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('');
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(1000);
  const [sortOption, setSortOption] = useState('suggested');

  const searchBarQuery = useSelector((state) => {
    return state.searchBarQuery;
  });

  const selectedCategoryName = useSelector((state) => {
    return state.selectedCategoryName;
  });

  const dispatch = useDispatch();

  const getAllExperiences = async () => {
    try {
      setIsLoading(true);

      const response = await fetch(`${backendUrl}/Experience/getExperiences`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
        setExperiences(data.experiences);
        setFilteredExperiences(data.experiences);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
      setIsLoading(false);
    }
  };

  const getAllCategories = async () => {
    try {
      const response = await fetch(`${backendUrl}/Category`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setCategories(data.categories);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const searchExperiences = () => {
    const filtExperiences = experiences.filter(
      (exp) =>
        exp.title.toLowerCase().includes(searchBar.toLowerCase()) &&
        (exp.category.name === selectedCategory ||
          exp.category.name.includes(selectedCategory)) &&
        exp.price >= minPrice &&
        exp.price <= maxPrice
    );

    switch (sortOption) {
      case 'suggested':
        break;

      case 'priceUp':
        filtExperiences.sort((a, b) => a.price - b.price);
        break;

      case 'priceDown':
        filtExperiences.sort((a, b) => b.price - a.price);
        break;

      case 'latest':
        filtExperiences.sort(
          (a, b) => new Date(b.lastEditDate) - new Date(a.lastEditDate)
        );
        break;

      case 'lessRecent':
        filtExperiences.sort(
          (a, b) => new Date(a.lastEditDate) - new Date(b.lastEditDate)
        );
        break;

      default:
        break;
    }

    setFilteredExperiences(filtExperiences);
  };

  const resetFilters = () => {
    setSearchBar('');
    setSelectedCategory('');
    setMinPrice(0);
    setMaxPrice(1000);
  };

  useEffect(() => {
    getAllExperiences();
    getAllCategories();

    setSearchBar(searchBarQuery);
    setSelectedCategory(selectedCategoryName);

    dispatch(setSelectedCategoryName(''));
    dispatch(setSearchBarQuery(''));
  }, []);

  useEffect(() => {
    searchExperiences();
  }, [
    experiences,
    searchBar,
    selectedCategory,
    minPrice,
    maxPrice,
    sortOption,
  ]);

  useEffect(() => {
    if (searchBarQuery !== '') {
      setSearchBar(searchBarQuery);
      dispatch(setSearchBarQuery(''));
    }

    if (selectedCategoryName !== '') {
      setSelectedCategory(selectedCategoryName);
      dispatch(setSelectedCategoryName(''));
    }
  }, [searchBarQuery, selectedCategoryName]);

  return (
    <div className='max-w-7xl min-h-[80vh] mx-auto mb-8 mt-6 px-6 xl:px-0'>
      <div>
        <h1 className='text-3xl font-bold mb-3'>Esperienze</h1>
        <p className='text-gray-500 max-w-md'>
          Esplora la nostra collezione di avventure incredibili. Dalle
          esperienze adrenaliniche al relax, trova l atua prossima avventura.
        </p>
      </div>

      {/* search area */}
      <div className='bg-white p-4 rounded-2xl my-6 shadow-xs'>
        <div className='xs:flex justify-between items-center gap-2'>
          <div className='relative grow flex items-center me-3 max-xs:mb-4'>
            <input
              className='bg-background border border-gray-800/30 rounded-xl py-2 ps-10 w-full text-sm xs:text-base'
              placeholder='Cerca esperienze...'
              value={searchBar}
              onChange={(e) => {
                setSearchBar(e.target.value.toLowerCase());
              }}
            />
            <Search className='absolute start-2 top-1/2 -translate-y-1/2' />
          </div>

          <div className='inline-block me-3 xs:me-0'>
            <Button
              variant='outline'
              icon={<Funnel className='w-3.5 h-3.5 text-gray-700' />}
              onClick={() => {
                setShowFilters(!showFilters);
              }}
            >
              Filtri
            </Button>
          </div>

          <div className='inline-block'>
            <Button variant='primary' onClick={searchExperiences}>
              Cerca
            </Button>
          </div>
        </div>

        {showFilters && (
          <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 justify-start items-end gap-8 border-t border-gray-500/30 mt-6 pt-4'>
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
                {categories.length > 0 &&
                  categories.map((category) => (
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
        {!isLoading && (
          <div className='sm:flex justify-between items-center my-6'>
            <h4 className='text-xl font-semibold mb-4'>
              {filteredExperiences.length} esperienze trovate
            </h4>

            <select
              name=''
              id=''
              className='bg-background border border-gray-800/30 rounded-xl py-2 px-3'
              value={sortOption}
              onChange={(e) => {
                setSortOption(e.target.value);
              }}
              hidden={filteredExperiences.length === 0 ? true : false}
            >
              <option value='suggested'>Consigliati</option>
              <option value='priceUp'>Prezzo: crescente</option>
              <option value='priceDown'>Prezzo: decrescente</option>
              <option value='latest'>Più recenti</option>
              <option value='lessRecent'>Meno recenti</option>
            </select>
          </div>
        )}

        {isLoading && <ExperiencesSkeletonLoader />}

        {filteredExperiences.length > 0 && (
          <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6'>
            {/* elenco esperienze */}
            {filteredExperiences.map((exp) => (
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
