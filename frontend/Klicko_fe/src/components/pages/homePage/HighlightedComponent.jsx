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
        <div className='max-w-7xl mx-auto my-18'>
          <p className='text-[#F97415] font-semibold mb-2'>
            Esperienze in evidenza
          </p>
          <div className='flex justify-between items-center'>
            <h2 className=' text-4xl font-bold'>
              Le nostre migliori avventure
            </h2>
            <Button variant='outline' size='md'>
              Vedi tutte
            </Button>
          </div>

          <div className='columns-3 gap-8 mt-10'>
            {highlightedExperiences.map((experience) => {
              return (
                // card
                <div
                  key={experience.experienceId}
                  className='relative rounded-lg overflow-hidden shadow-md hover:shadow-xl hover:bottom-2 ease-in h-100'
                >
                  {/* card top */}
                  <div className='relative h-[230px] overflow-hidden'>
                    <img
                      // src={`http://localhost:5000${experience.coverImage}`}
                      src={`https://localhost:7235/uploads/${experience.coverImage}`}
                      className='z-0'
                    />
                    <span className='absolute top-2 start-2 text-white text-xs px-2 py-1 bg-[#F97415] rounded-full z-10'>
                      In evidenza
                    </span>
                  </div>

                  {/* card bottom */}
                  <div className='p-4'>
                    <div className='flex justify-between items-center'>
                      <span className='text-gray-500 text-sm'>
                        {experience.category.name}
                      </span>
                      <span className='text-[#F97415] text-xl font-semibold'>
                        {experience.price},00 €
                      </span>
                    </div>
                    <h4 className='text-lg font-semibold'>
                      {experience.title}
                    </h4>
                    <p className='text-gray-600 text-[0.9rem]'>
                      {experience.descriptionShort}
                    </p>
                    <div className='flex justify-between'>
                      <span className='flex items-center gap-1 text-sm text-gray-500'>
                        <MapPin className='h-4 w-4' />
                        {experience.place}
                      </span>
                      <span className='flex items-center gap-1 text-sm text-gray-500'>
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
