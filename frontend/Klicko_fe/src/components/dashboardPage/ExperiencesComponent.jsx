import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { Funnel, Pencil, Plus, Search, Trash2 } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

const ExperiencesComponent = () => {
  const [experiences, setExperiences] = useState([]);

  const navigate = useNavigate();

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

        console.log(data);

        setExperiences(data.experiences);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getAllExperiences();
  }, []);

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

      <div className='flex justify-between items-center gap-4 mb-8'>
        <div className='relative grow'>
          <input
            type='text'
            placeholder='Cerca esperienze...'
            className='bg-background border border-gray-400/30 rounded-xl py-2 ps-10 w-full'
          />
          <Search className='absolute top-1/2 left-3 -translate-y-1/2 w-5 h-5 pb-0.5' />
        </div>

        <Button variant='outline' icon={<Funnel className='w-4 h-4' />}>
          Filtri
        </Button>
      </div>

      <div>
        {experiences.length > 0 && (
          <div>
            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='col-span-8 text-gray-500 text-sm font-medium text-start ps-3'>
                    Esperienza
                  </th>
                  <th className='col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Categoria
                  </th>
                  <th className='col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Prezzo
                  </th>
                  <th className='col-span-4 text-gray-500 text-sm font-medium text-start'>
                    Luogo
                  </th>
                  <th className='col-span-3 text-gray-500 text-sm font-medium text-start'>
                    Data
                  </th>
                  <th className='col-span-3 text-gray-500 text-sm font-medium text-end pe-3'>
                    Azioni
                  </th>
                </tr>
              </thead>
              <tbody>
                {/* TODO: fare il map delle esperienze */}
                {experiences.map((exp) => (
                  <tr
                    key={exp.experienceId}
                    className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 px-2 last-of-type:border-0'
                  >
                    <td className='col-span-8 flex justify-start items-center gap-3'>
                      <div>
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

                    <td className='col-span-3'>
                      <p className='text-sm'>{exp.category.name}</p>
                    </td>

                    <td className='col-span-3'>
                      <p className='text-sm'>
                        {exp.price.toFixed(2).replace('.', ',')} €
                      </p>
                    </td>

                    <td className='col-span-4'>
                      <p className='text-sm'>{exp.place}</p>
                    </td>

                    <td className='col-span-3'>
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

                    <td className='col-span-3'>
                      <div className='flex justify-end items-center gap-4 pe-3'>
                        <Pencil className='w-4 h-4 text-gray-600' />
                        <Trash2 className='w-4 h-4 text-red-600' />
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
      </div>
    </>
  );
};

export default ExperiencesComponent;
