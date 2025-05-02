import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import ExperienceCard from '../ExperienceCard';
import { Link } from 'react-router-dom';
import { toast } from 'sonner';

const HighlightedComponent = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [highlightedExperiences, setHighlightedExperiences] = useState([]);

  const getExperiences = async () => {
    try {
      const response = await fetch(`${backendUrl}/Experience/Highlighted`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();
        setHighlightedExperiences(data.experiences);
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

  useEffect(() => {
    getExperiences();
  }, []);

  return (
    <>
      {highlightedExperiences.length > 0 && (
        <div className='max-w-7xl mx-auto my-18 px-4 xl:px-0'>
          <p className='text-sm md:text-base text-secondary font-semibold mb-2'>
            Esperienze in evidenza
          </p>
          <div className='flex justify-between items-center'>
            <h2 className='text-2xl 2xl:text-4xl font-bold'>
              Le nostre migliori avventure
            </h2>
            <Button variant='outline' size='md'>
              <Link to='/experiences'>Vedi tutte</Link>
            </Button>
          </div>

          <div className='grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mt-10'>
            {highlightedExperiences.map((experience) => {
              return (
                <ExperienceCard
                  key={experience.experienceId}
                  experience={experience}
                />
              );
            })}
          </div>
        </div>
      )}
    </>
  );
};

export default HighlightedComponent;
