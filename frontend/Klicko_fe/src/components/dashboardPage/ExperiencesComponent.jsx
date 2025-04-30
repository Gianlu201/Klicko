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

const ExperiencesComponent = () => {
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

  const getAllExperiences = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch('https://localhost:7235/api/Experience', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();

        console.log(data);

        setExperiences(data.experiences);
        setFilteredExperiences(data.experiences);
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

        console.log(data);

        setCategories(data.categories);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
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

    console.log(experiencesList);
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
        `https://localhost:7235/api/Experience/softDelete/${experienceId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        // const data = await response.json();
        toast.success(`${selectedExperience.title} rimossa con successo!`);
        getAllExperiences();
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
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
        `https://localhost:7235/api/Experience/restoreExperience/${experienceId}`,
        {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        // const data = await response.json();
        toast.success(`${selectedExperience.title} ripristinata!`);
        getAllExperiences();
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const hardDeleteExperience = async (experienceId) => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const response = await fetch(
        `https://localhost:7235/api/Experience/${experienceId}`,
        {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        // const data = await response.json();
        toast.success(`${selectedExperience.title} rimossa con successo!`);
        getAllExperiences();
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getAllExperiences();
    getAllCategories();
  }, []);

  useEffect(() => {
    searchExperiences();
  }, [searchBar, selectedCategory, minPrice, maxPrice]);

  return (
    <>
      <div className='flex justify-between items-center mb-6'>
        <h2 className='text-2xl font-bold mb-2'>Gestione esperienze</h2>
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
        <div className='flex justify-between items-center gap-4'>
          <div className='relative grow'>
            <input
              type='text'
              placeholder='Cerca esperienze...'
              className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
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
          <div className='grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 justify-start items-end gap-8 mt-4'>
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
                {categories.map((category) => (
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
        {filteredExperiences.length > 0 && (
          <div>
            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='col-span-10 md:col-span-8 text-gray-500 text-sm font-medium text-start ps-3'>
                    Esperienza
                  </th>
                  <th className='col-span-5 md:col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Categoria
                  </th>
                  <th className='col-span-5 md:col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Prezzo
                  </th>
                  <th className='hidden md:block col-span-4 text-gray-500 text-sm font-medium text-start'>
                    Luogo
                  </th>
                  <th className='hidden md:block col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Data
                  </th>
                  <th className='col-span-4 md:col-span-3 text-gray-500 text-sm font-medium text-end pe-3'>
                    Azioni
                  </th>
                </tr>
              </thead>
              <tbody>
                {filteredExperiences.map((exp) => (
                  <tr
                    key={exp.experienceId}
                    className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 px-2 last-of-type:border-0'
                  >
                    <td className='col-span-10 md:col-span-8 flex justify-start items-center gap-3'>
                      <div className='hidden md:block'>
                        <div className='h-[40px] aspect-square rounded overflow-hidden'>
                          <img
                            src={`https://localhost:7235/uploads/${exp.coverImage}`}
                            alt={exp.title}
                            className='w-full h-full object-cover'
                          />
                        </div>
                      </div>
                      <div className=''>
                        <p className='text-sm font-semibold'>{exp.title}</p>
                        <p className='text-gray-500 text-xs'>{exp.organiser}</p>
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

                    <td className='hidden md:block col-span-4'>
                      <p className='text-xs lg:text-sm'>{exp.place}</p>
                    </td>

                    <td className='hidden md:block col-span-3'>
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
                      <div className='flex justify-end items-center gap-4 pe-3'>
                        <Pencil
                          className='w-4 h-4 md:w-4 md:h-4 text-gray-600 cursor-pointer'
                          onClick={() => {
                            navigate(
                              `/dashboard/experiences/edit/${exp.experienceId}`
                            );
                          }}
                        />
                        {exp.isDeleted ? (
                          <div>
                            <ArchiveRestore
                              className='w-4 h-4 md:w-4 md:h-4 text-green-600 cursor-pointer mb-2'
                              onClick={() => {
                                setOpenRestoreModal(true);
                                setSelectedExperience(exp);
                              }}
                            />

                            <OctagonXIcon
                              className='w-4 h-4 md:w-4 md:h-4 text-red-600 cursor-pointer'
                              onClick={() => {
                                setOpenHardDeleteModal(true);
                                setSelectedExperience(exp);
                              }}
                            />
                          </div>
                        ) : (
                          <Trash2
                            className='w-4 h-4 md:w-4 md:h-4 text-red-600 cursor-pointer'
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
        )}

        {experiences.length === 0 && (
          <div className='flex flex-col items-center justify-center gap-3 py-12'>
            <h4 className='text-xl font-semibold'>Nesuna esperienza trovata</h4>
            <p className='text-gray-500 font-normal'>
              Nessuna esperienza corrispondente ai criteri di ricerca.
            </p>
            <Button variant='primary'>Crea la tua prima esperienza</Button>
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
              <CircleAlert className='mx-auto mb-4 h-14 w-14 text-red-500' />
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
              <CircleFadingArrowUp className='mx-auto mb-4 h-14 w-14 text-green-500' />
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
              <CircleAlert className='mx-auto mb-4 h-14 w-14 text-red-500' />
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
