import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { Clock, MapPin } from 'lucide-react';
import ExperienceCard from '../ExperienceCard';
import { Link } from 'react-router-dom';
import { toast } from 'sonner';
import HomeSkeletonLoader from '../ui/HomeSkeletonLoader';

const PopularComponent = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const [isLoading, setIsLoading] = useState(true);
  const [popularExperiences, setPopularExperiences] = useState([]);

  const getExperiences = async () => {
    try {
      setIsLoading(true);

      const response = await fetch(`${backendUrl}/Experience/Popular`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();

        setIsLoading(false);
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
      setIsLoading(false);
    }
  };

  useEffect(() => {
    getExperiences();
  }, []);

  return (
    <>
      <div className='px-4 mx-auto max-w-7xl my-18 xl:px-0'>
        <p className='text-sm md:text-base text-[#19aeff] font-semibold mb-2'>
          Esperienze in evidenza
        </p>
        <div className='items-center justify-between xs:flex'>
          <h2 className='text-xl font-bold xs:text-2xl 2xl:text-4xl'>
            Le nostre avventure più popolari
          </h2>
          <Button variant='outline' size='md' className='ms-auto xs:ms-0'>
            <Link to='/experiences'>Vedi tutte</Link>
          </Button>
        </div>

        {isLoading && <HomeSkeletonLoader />}

        {popularExperiences.length > 0 && (
          <div className='grid grid-cols-1 gap-8 mt-10 md:grid-cols-2 lg:grid-cols-4'>
            {popularExperiences.map((experience) => {
              return (
                <ExperienceCard
                  key={experience.experienceId}
                  experience={experience}
                />
              );
            })}
          </div>
        )}
      </div>
    </>
  );
};

export default PopularComponent;
