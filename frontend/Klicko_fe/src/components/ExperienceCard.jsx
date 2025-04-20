import { Clock, MapPin } from 'lucide-react';
import React from 'react';
import { Link } from 'react-router-dom';

const ExperienceCard = ({ experience, className }) => {
  return (
    <>
      {experience && (
        <Link
          to={`/experiences/detail/${experience.experienceId}`}
          className={`block relative rounded-lg overflow-hidden shadow-md hover:shadow-xl hover:-translate-y-3 duration-700 ease-in-out cursor-pointer ${
            className ? className : ' '
          }`}
        >
          {/* card top */}
          <div className='relative aspect-16/9 overflow-hidden'>
            <img
              src={`https://localhost:7235/uploads/${experience.coverImage}`}
              className='absolute top-1/2 start-1/2 -translate-y-1/2 -translate-x-1/2 z-0 w-full h-full'
            />
            {experience.isInEvidence ? (
              <span className='absolute top-2 start-2 text-white text-xs font-semibold px-2 py-1 bg-secondary rounded-full z-10'>
                In evidenza
              </span>
            ) : (
              experience.isPopular && (
                <span className='absolute top-2 start-2 text-white text-xs font-semibold px-2 py-1 bg-primary rounded-full z-10'>
                  Popolare
                </span>
              )
            )}
            {experience.sale > 0 && (
              <span className='absolute top-0 right-0 z-10 w-full transform rotate-45 translate-x-14 -translate-y-4 bg-red-600 text-white text-sm font-bold text-end py-1.5 pe-18 shadow-md'>
                -{experience.sale}%
              </span>
            )}
          </div>

          {/* card bottom */}
          <div className='p-4'>
            <div className='flex justify-between items-center mb-2'>
              <span className='text-gray-500 text-sm'>
                {experience.category.name}
              </span>
              <div className='flex items-center'>
                <span
                  className={`font-semibold ${
                    experience.sale > 0
                      ? 'line-through text-gray-500 me-2'
                      : 'text-secondary text-xl'
                  }`}
                >
                  {experience.price.toFixed(2).replace('.', ',')} €
                </span>
                {experience.sale > 0 && (
                  <span className={`font-semibold text-secondary text-xl`}>
                    {((experience.price * (100 - experience.sale)) / 100)
                      .toFixed(2)
                      .replace('.', ',')}{' '}
                    €
                  </span>
                )}
              </div>
            </div>
            <h4 className='text-lg font-semibold mb-2'>{experience.title}</h4>
            <p className='text-gray-600 text-[0.9rem]'>
              {experience.descriptionShort}
            </p>
            <div className='flex justify-between my-2'>
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
        </Link>
      )}
    </>
  );
};

export default ExperienceCard;
