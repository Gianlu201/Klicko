import { Clock, MapPin } from 'lucide-react';
import React from 'react';
import { Link } from 'react-router-dom';

const ExperienceCard = ({ experience, className }) => {
  return (
    <>
      {experience && (
        <Link
          to={`/experiences/detail/${experience.experienceId}`}
          className={`block relative rounded-lg overflow-hidden shadow-md hover:shadow-xl hover:-translate-y-3 duration-700 ease-in-out cursor-pointer pb-8 ${
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
              <span className='absolute top-2 start-2 text-white md:text-xs font-semibold px-2 py-1 bg-secondary rounded-full z-10'>
                In evidenza
              </span>
            ) : (
              experience.isPopular && (
                <span className='absolute top-2 start-2 text-white md:text-xs font-semibold px-2 py-1 bg-primary rounded-full z-10'>
                  Popolare
                </span>
              )
            )}
            {experience.sale > 0 && (
              <span className='absolute top-0 right-0 z-10 w-full transform rotate-45 translate-x-24 -translate-y-18 sm:translate-x-20 sm:-translate-y-8 md:translate-x-20 md:-translate-y-10 lg:translate-x-18 lg:-translate-y-4 xl:translate-x-16 bg-red-600 text-white text-sm font-bold text-end py-1.5 pe-18 shadow-md'>
                -{experience.sale}%
              </span>
            )}
          </div>

          {/* card bottom */}
          <div className='p-4'>
            <div className='flex justify-between items-center mb-2'>
              <span className='text-gray-500 font-medium md:font-normal md:text-sm'>
                {experience.category.name}
              </span>
              <div className='flex items-center'>
                <span
                  className={`font-bold md:font-semibold ${
                    experience.sale > 0
                      ? 'line-through text-gray-500 me-2 text-lg md:text-base'
                      : 'text-secondary text-2xl md:text-lg'
                  }`}
                >
                  {experience.price.toFixed(2).replace('.', ',')} €
                </span>
                {experience.sale > 0 && (
                  <span
                    className={`font-bold md:font-semibold text-secondary text-2xl md:text-lg`}
                  >
                    {((experience.price * (100 - experience.sale)) / 100)
                      .toFixed(2)
                      .replace('.', ',')}{' '}
                    €
                  </span>
                )}
              </div>
            </div>
            <h4 className='text-xl md:text-lg font-semibold mb-2 line-clamp-2'>
              {experience.title}
            </h4>
            <p className='text-gray-600 md:text-[0.9rem] line-clamp-2'>
              {experience.descriptionShort}
            </p>

            <div className='absolute w-full start-4 bottom-2'>
              <div className='flex justify-between pe-8 my-2'>
                <span className='flex items-center gap-1 md:text-sm text-gray-500'>
                  <MapPin className='h-4 w-4' />
                  {experience.place}
                </span>
                <span className='flex items-center gap-1 md:text-sm text-gray-500'>
                  <Clock className='h-4 w-4' />
                  {experience.duration}
                </span>
              </div>
            </div>
          </div>
        </Link>
      )}
    </>
  );
};

export default ExperienceCard;
