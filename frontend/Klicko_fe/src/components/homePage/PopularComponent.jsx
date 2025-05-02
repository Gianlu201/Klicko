import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { Clock, MapPin } from 'lucide-react';
import ExperienceCard from '../ExperienceCard';
import { Link } from 'react-router-dom';
import { toast } from 'sonner';

const PopularComponent = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [popularExperiences, setPopularExperiences] = useState([]);

  const getExperiences = async () => {
    try {
      const response = await fetch(`${backendUrl}/Experience/Popular`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setPopularExperiences(data.experiences);
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
      {popularExperiences.length > 0 && (
        <div className='max-w-7xl mx-auto my-18 px-4 xl:px-0'>
          <p className='text-sm md:text-base text-[#19aeff] font-semibold mb-2'>
            Esperienze in evidenza
          </p>
          <div className='xs:flex justify-between items-center'>
            <h2 className='text-xl xs:text-2xl 2xl:text-4xl font-bold'>
              Le nostre avventure più popolari
            </h2>
            <Button variant='outline' size='md' className='ms-auto xs:ms-0'>
              <Link to='/experiences'>Vedi tutte</Link>
            </Button>
          </div>

          <div className='grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8 mt-10'>
            {popularExperiences.map((experience) => {
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

export default PopularComponent;
