import React, { useEffect, useState } from 'react';
import Button from '../../ui/Button';
import { Clock, MapPin } from 'lucide-react';
import ExperienceCard from '../../ExperienceCard';
import { Link } from 'react-router-dom';

const PopularComponent = () => {
  const [popularExperiences, setPopularExperiences] = useState([]);

  const getExperiences = async () => {
    try {
      const response = await fetch(
        'https://localhost:7235/api/Experience/Popular',
        {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      if (response.ok) {
        const data = await response.json();
        console.log(data);

        setPopularExperiences(data.experiences);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Errore');
    }
  };

  useEffect(() => {
    getExperiences();
  }, []);

  return (
    <>
      {popularExperiences.length > 0 && (
        <div className='max-w-7xl mx-auto my-18'>
          <p className='text-[#19aeff] font-semibold mb-2'>
            Esperienze in evidenza
          </p>
          <div className='flex justify-between items-center'>
            <h2 className=' text-4xl font-bold'>
              Le nostre migliori avventure
            </h2>
            <Button variant='outline' size='md'>
              <Link to='/experiences'>Vedi tutte</Link>
            </Button>
          </div>

          <div className='columns-4 gap-8 mt-10'>
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
