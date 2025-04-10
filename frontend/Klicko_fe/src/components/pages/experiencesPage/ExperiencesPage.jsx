import { Funnel, Search } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import Button from '../../ui/Button';

const ExperiencesPage = () => {
  const [experiences, setExperiences] = useState([]);

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
    <div className='max-w-7xl mx-auto'>
      <div>
        <h1 className='text-3xl font-bold'>Esperienze</h1>
        <p className='text-gray-500 max-w-1/2'>
          Esplora la nostra collezione di avventure incredibili. Dalle
          esperienze adrenaliniche al relax, trova l atua prossima avventura.
        </p>
      </div>

      {/* search area */}
      <div className='bg-white flex justify-between items-center gap-2 p-4 rounded-2xl'>
        <div className='relative grow flex items-center me-3'>
          <input
            className='bg-background border border-gray-800/30 rounded-xl py-2 ps-10 w-full'
            placeholder='Cerca esperienze...'
          />
          <Search className='absolute start-2 top-1/2 -translate-y-1/2' />
        </div>
        <Button
          variant='outline'
          icon={<Funnel className='w-3.5 h-3.5 text-gray-700' />}
        >
          Filtri
        </Button>
        <Button variant='primary'>Cerca</Button>
      </div>

      <div>
        <div className='flex justify-between items-center'>
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
          <div>
            {/* elenco esperienze */}
            {experiences.map((exp) => (
              <div key={exp.id}>{exp.title}</div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default ExperiencesPage;
