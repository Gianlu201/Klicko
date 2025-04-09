import React, { useEffect, useState } from 'react';
import Button from '../../ui/Button';
import { Clock, MapPin } from 'lucide-react';

const HighlightedComponent = () => {
  const [highlightedExperiences, setHighlightedExperiences] = useState([]);

  const getExperiences = async () => {
    try {
      const response = await fetch(
        'https://localhost:7235/api/Experience/Highlighted',
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

        setHighlightedExperiences(data.experiences);
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
      {highlightedExperiences.length > 0 && (
        <div className='max-w-7xl mx-auto mt-16'>
          <p>Esperienze in evidenza</p>
          <div className='flex justify-between items-center'>
            <h2 className=' text-4xl font-bold'>
              Le nostre migliori avventure
            </h2>
            <Button variant='outline' size='md'>
              Vedi tutte
            </Button>
          </div>

          <div className='columns-3 gap-8'>
            {highlightedExperiences.map((experience) => {
              return (
                // card
                <div
                  key={experience.experienceId}
                  className='rounded-lg shadow-md p-4'
                >
                  {/* card top */}
                  <div>
                    <img />
                    <span>In evidenza</span>
                  </div>

                  {/* card bottom */}
                  <div>
                    <div className='flex justify-between'>
                      <span>{experience.category.name}</span>
                      <span>{experience.price}</span>
                    </div>
                    <h4>{experience.title}</h4>
                    <p>{experience.descriptionShort}</p>
                    <div className='flex justify-between'>
                      <span className='flex items-center gap-1'>
                        <MapPin className='h-4 w-4' />
                        {experience.place}
                      </span>
                      <span className='flex items-center gap-1'>
                        <Clock className='h-4 w-4' />
                        {experience.duration}
                      </span>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      )}
    </>
  );
};

export default HighlightedComponent;
