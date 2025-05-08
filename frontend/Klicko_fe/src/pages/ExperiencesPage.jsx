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
        <h1 className='mb-3 text-3xl font-bold'>Esperienze</h1>
        <p className='max-w-md text-gray-500'>
          Esplora la nostra collezione di avventure incredibili. Dalle
          esperienze adrenaliniche al relax, trova l atua prossima avventura.
        </p>
      </div>

      {/* search area */}
      <div className='p-4 my-6 bg-white shadow-xs rounded-2xl'>
        <div className='items-center justify-between gap-2 xs:flex'>
          <div className='relative flex items-center grow me-3 max-xs:mb-4'>
            <input
              className='w-full py-2 text-sm border bg-background border-gray-800/30 rounded-xl ps-10 xs:text-base'
              placeholder='Cerca esperienze...'
              value={searchBar}
              onChange={(e) => {
                setSearchBar(e.target.value.toLowerCase());
              }}
            />
            <Search className='absolute -translate-y-1/2 start-2 top-1/2' />
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
          <div className='grid items-end justify-start grid-cols-1 gap-8 pt-4 mt-6 border-t sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 border-gray-500/30'>
            <div className='flex flex-col items-start justify-start gap-2'>
              <span className='text-sm font-medium'>Categoria</span>
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
            <div className='flex flex-col items-start justify-start gap-2'>
              <span className='text-sm font-medium'>Prezzo minimo</span>
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
            <div className='flex flex-col items-start justify-start gap-2'>
              <span className='text-sm font-medium'>Prezzo massimo</span>
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
          <div className='items-center justify-between my-6 sm:flex'>
            <h4 className='mb-4 text-xl font-semibold'>
              {filteredExperiences.length} esperienze trovate
            </h4>

            <select
              name=''
              id=''
              className='px-3 py-2 border bg-background border-gray-800/30 rounded-xl'
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
          <div className='grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4'>
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
