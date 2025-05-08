import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import {
  ArchiveRestore,
  CircleAlert,
  CircleFadingArrowUp,
  Funnel,
  OctagonXIcon,
  Pencil,
  Plus,
  Search,
  Trash2,
  X,
} from 'lucide-react';
import { useNavigate } from 'react-router-dom';

import { Modal, ModalBody, ModalHeader } from 'flowbite-react';
import { toast } from 'sonner';
import { cartModified } from '../../redux/actions';
import { useDispatch, useSelector } from 'react-redux';
import SkeletonList from '../ui/SkeletonList';

const ExperiencesComponent = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isAuthorized, setIsAuthorized] = useState(false);

  const [isLoading, setIsLoading] = useState(true);
  const [experiences, setExperiences] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filteredExperiences, setFilteredExperiences] = useState([]);
  const [searchBar, setSearchBar] = useState('');
  const [showFilters, setShowFilters] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState('');
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(1000);

  const [openSoftDeleteModal, setOpenSoftDeleteModal] = useState(false);
  const [openRestoreModal, setOpenRestoreModal] = useState(false);
  const [openHardDeleteModal, setOpenHardDeleteModal] = useState(false);
  const [selectedExperience, setSelectedExperience] = useState(null);

  const navigate = useNavigate();

  const dispatch = useDispatch();

  const profile = useSelector((state) => {
    return state.profile;
  });

  const checkAuthorization = () => {
    if ('admin, seller'.includes(profile.role.toLowerCase())) {
      setIsAuthorized(true);
    } else {
      navigate('/unauthorized');
    }
  };

  const getAllExperiences = async () => {
    try {
      setIsLoading(true);

      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(`${backendUrl}/Experience`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
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
    const experiencesList = experiences.filter(
      (exp) =>
        (exp.title.toLowerCase().includes(searchBar.toLowerCase()) ||
          exp.place.toLowerCase().includes(searchBar.toLowerCase())) &&
        (exp.category.name === selectedCategory ||
          exp.category.name.includes(selectedCategory)) &&
        exp.price >= minPrice &&
        exp.price <= maxPrice
    );

    setFilteredExperiences(experiencesList);
  };

  const resetFilters = () => {
    setSearchBar('');
    setSelectedCategory('');
    setMinPrice(0);
    setMaxPrice(1000);
  };

  const softDeleteExperience = async (experienceId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `${backendUrl}/Experience/softDelete/${experienceId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Modifica effettuata!</p>
            <p>{selectedExperience.title} rimossa con successo!</p>
          </>
        );

        getAllExperiences();
        dispatch(cartModified());
      } else {
        throw new Error(`Errore nella modifica dell'esperienza!`);
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

  const restoreExperience = async (experienceId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `${backendUrl}/Experience/restoreExperience/${experienceId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Modifica effettuata!</p>
            <p>{selectedExperience.title} ripristinata!</p>
          </>
        );
        getAllExperiences();
      } else {
        throw new Error(`Errore nella modifica dell'esperienza!`);
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

  const hardDeleteExperience = async (experienceId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(`${backendUrl}/Experience/${experienceId}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        toast.success(
          <>
            <p className='font-bold'>Modifica effettuata!</p>
            <p>{selectedExperience.title} rimossa con successo!</p>
          </>
        );
        getAllExperiences();
        dispatch(cartModified());
      } else {
        throw new Error(`Errore nella modifica dell'esperienza!`);
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

  useEffect(() => {
    if (profile.role) {
      checkAuthorization();

      if (isAuthorized) {
        getAllExperiences();
        getAllCategories();
      }
    }
  }, [profile, isAuthorized]);

  useEffect(() => {
    searchExperiences();
  }, [searchBar, selectedCategory, minPrice, maxPrice]);

  return (
    <>
      <div className='items-center justify-between mb-6 xs:flex'>
        <h2 className='mb-2 text-xl font-bold xs:text-2xl'>
          Gestione esperienze
        </h2>
        <Button
          variant='primary'
          icon={<Plus />}
          onClick={() => {
            navigate('/dashboard/experiences/add');
          }}
        >
          Nuova esperienza
        </Button>
      </div>

      <div className='mb-12'>
        <div className='items-center justify-between gap-4 xs:flex'>
          <div className='relative grow max-xs:mb-4'>
            <input
              type='text'
              placeholder='Cerca esperienze...'
              className='w-full py-2 border bg-background border-gray-400/30 rounded-xl ps-10'
              value={searchBar}
              onChange={(e) => {
                setSearchBar(e.target.value);
              }}
            />
            <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
          </div>

          <Button
            variant='outline'
            icon={<Funnel className='w-4 h-4' />}
            onClick={() => {
              setShowFilters(!showFilters);
            }}
          >
            Filtri
          </Button>
        </div>
        {showFilters && (
          <div className='grid grid-cols-1 min-[480px]:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 justify-start items-end gap-8 mt-4'>
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
                {categories.map((category) => (
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
        {isLoading ? (
          <SkeletonList />
        ) : filteredExperiences.length > 0 ? (
          <div className='overflow-x-auto'>
            <table className='w-full'>
              <thead>
                <tr className='grid gap-4 pb-3 border-b grid-cols-24 border-gray-400/30'>
                  <th className='col-span-10 text-sm font-medium text-gray-500 md:col-span-8 text-start ps-3'>
                    Esperienza
                  </th>
                  <th className='col-span-5 text-sm font-medium text-gray-500 md:col-span-3 text-start'>
                    Categoria
                  </th>
                  <th className='col-span-5 text-sm font-medium text-gray-500 md:col-span-3 text-start'>
                    Prezzo
                  </th>
                  <th className='hidden col-span-4 text-sm font-medium text-gray-500 md:block text-start'>
                    Luogo
                  </th>
                  <th className='hidden col-span-3 text-sm font-medium text-gray-500 md:block text-start'>
                    Data
                  </th>
                  <th className='col-span-4 text-sm font-medium text-gray-500 md:col-span-3 text-end pe-3'>
                    Azioni
                  </th>
                </tr>
              </thead>
              <tbody>
                {filteredExperiences.map((exp) => (
                  <tr
                    key={exp.experienceId}
                    className='grid items-center gap-4 px-2 py-3 border-b grid-cols-24 hover:bg-gray-100 border-gray-400/30 last-of-type:border-0'
                  >
                    <td className='flex items-center justify-start col-span-10 gap-3 md:col-span-8'>
                      <div className='hidden md:block'>
                        <div className='h-[40px] aspect-square rounded overflow-hidden'>
                          <img
                            src={`https://klicko-backend-api.azurewebsites.net/uploads/${exp.coverImage}`}
                            alt={exp.title}
                            className='object-cover w-full h-full'
                          />
                        </div>
                      </div>
                      <div className=''>
                        <p className='text-sm font-semibold'>{exp.title}</p>
                        <p className='text-xs text-gray-500'>{exp.organiser}</p>
                      </div>
                    </td>

                    <td className='col-span-5 md:col-span-3'>
                      <p className='text-xs font-semibold lg:font-normal lg:text-sm'>
                        {exp.category.name}
                      </p>
                    </td>

                    <td className='col-span-5 md:col-span-3'>
                      <p className='text-sm'>
                        {exp.price.toFixed(2).replace('.', ',')} €
                      </p>
                    </td>

                    <td className='hidden col-span-4 md:block'>
                      <p className='text-xs lg:text-sm'>{exp.place}</p>
                    </td>

                    <td className='hidden col-span-3 md:block'>
                      <p className='text-sm'>
                        {new Date(exp.lastEditDate).toLocaleDateString(
                          'it-IT',
                          {
                            year: 'numeric',
                            month: 'long',
                            day: 'numeric',
                          }
                        )}
                      </p>
                    </td>

                    <td className='col-span-4 md:col-span-3'>
                      <div className='flex items-center justify-end gap-4 max-sm:flex-col pe-3'>
                        <Pencil
                          className='w-4 h-4 text-gray-600 cursor-pointer md:w-4 md:h-4'
                          onClick={() => {
                            navigate(
                              `/dashboard/experiences/edit/${exp.experienceId}`
                            );
                          }}
                        />
                        {exp.isDeleted ? (
                          <div>
                            <ArchiveRestore
                              className='w-4 h-4 mb-2 text-green-600 cursor-pointer md:w-4 md:h-4'
                              onClick={() => {
                                setOpenRestoreModal(true);
                                setSelectedExperience(exp);
                              }}
                            />

                            <OctagonXIcon
                              className='w-4 h-4 text-red-600 cursor-pointer md:w-4 md:h-4'
                              onClick={() => {
                                setOpenHardDeleteModal(true);
                                setSelectedExperience(exp);
                              }}
                            />
                          </div>
                        ) : (
                          <Trash2
                            className='w-4 h-4 text-red-600 cursor-pointer md:w-4 md:h-4'
                            onClick={() => {
                              setOpenSoftDeleteModal(true);
                              setSelectedExperience(exp);
                            }}
                          />
                        )}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : (
          <div className='flex flex-col items-center justify-center gap-3 py-12'>
            <h4 className='text-xl font-semibold'>Nesuna esperienza trovata</h4>
            <p className='font-normal text-gray-500'>
              Nessuna esperienza corrispondente ai criteri di ricerca.
            </p>
            <Button
              variant='primary'
              onClick={() => {
                navigate('/dashboard/experiences/add');
              }}
            >
              Crea la tua prima esperienza
            </Button>
          </div>
        )}

        {/* Modale SoftDelete */}
        <Modal
          show={openSoftDeleteModal}
          size='md'
          onClose={() => {
            setSelectedExperience(null);
            setOpenSoftDeleteModal(false);
          }}
          popup
        >
          <ModalHeader className='bg-background rounded-t-2xl' />
          <ModalBody className='bg-background rounded-b-2xl'>
            <div className='text-center'>
              <CircleAlert className='mx-auto mb-4 text-red-500 h-14 w-14' />
              <h3 className='mb-5 text-lg font-medium'>
                Sicuro di voler eliminare questa esperienza?
              </h3>
              <p className='mb-8 text-gray-500'>
                Sarà eliminata dalla navigazione del sito, ma potrai
                ripristinarla da questa area
              </p>
              <div className='flex justify-center gap-4'>
                <Button
                  variant='danger'
                  onClick={() => {
                    softDeleteExperience(selectedExperience.experienceId);
                    setSelectedExperience(null);
                    setOpenSoftDeleteModal(false);
                  }}
                >
                  Elimina
                </Button>
                <Button
                  color='gray'
                  onClick={() => {
                    setSelectedExperience(null);
                    setOpenSoftDeleteModal(false);
                  }}
                >
                  Annulla
                </Button>
              </div>
            </div>
          </ModalBody>
        </Modal>

        {/* Modale Restore */}
        <Modal
          show={openRestoreModal}
          size='md'
          onClose={() => {
            setSelectedExperience(null);
            setOpenRestoreModal(false);
          }}
          popup
        >
          <ModalHeader className='bg-background rounded-t-2xl' />
          <ModalBody className='bg-background rounded-b-2xl'>
            <div className='text-center'>
              <CircleFadingArrowUp className='mx-auto mb-4 text-green-500 h-14 w-14' />
              <h3 className='mb-5 text-lg font-medium'>
                Sicuro di voler ripristinare questa esperienza?
              </h3>
              <p className='mb-8 text-gray-500'>
                Sarà nuovamente visibile nella navigazione del sito e potrà
                essere acquistata
              </p>
              <div className='flex justify-center gap-4'>
                <Button
                  variant='success'
                  onClick={() => {
                    restoreExperience(selectedExperience.experienceId);
                    setSelectedExperience(null);
                    setOpenRestoreModal(false);
                  }}
                >
                  Ripristina
                </Button>
                <Button
                  color='gray'
                  onClick={() => {
                    setSelectedExperience(null);
                    setOpenRestoreModal(false);
                  }}
                >
                  Annulla
                </Button>
              </div>
            </div>
          </ModalBody>
        </Modal>

        {/* Modale HardDelete */}
        <Modal
          show={openHardDeleteModal}
          size='md'
          onClose={() => {
            setSelectedExperience(null);
            setOpenHardDeleteModal(false);
          }}
          popup
        >
          <ModalHeader className='bg-background rounded-t-2xl' />
          <ModalBody className='bg-background rounded-b-2xl'>
            <div className='text-center'>
              <CircleAlert className='mx-auto mb-4 text-red-500 h-14 w-14' />
              <h3 className='mb-5 text-lg font-medium'>
                Sicuro di voler eliminare definitivamente questa esperienza?
              </h3>
              <p className='mb-8 text-gray-500'>
                Questa esperienza sarà eliminata definitivamente e non sarà
                possibile recuperarla, sicuro di voler procedere?
              </p>
              <div className='flex justify-center gap-4'>
                <Button
                  variant='danger'
                  onClick={() => {
                    hardDeleteExperience(selectedExperience.experienceId);
                    setSelectedExperience(null);
                    setOpenHardDeleteModal(false);
                  }}
                >
                  Elimina
                </Button>
                <Button
                  color='gray'
                  onClick={() => {
                    setSelectedExperience(null);
                    setOpenHardDeleteModal(false);
                  }}
                >
                  Annulla
                </Button>
              </div>
            </div>
          </ModalBody>
        </Modal>
      </div>
    </>
  );
};

export default ExperiencesComponent;
