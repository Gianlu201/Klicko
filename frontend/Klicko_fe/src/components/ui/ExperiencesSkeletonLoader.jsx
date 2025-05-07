import React from 'react';
import SkeletonCard from './SkeletonCard';

const ExperiencesSkeletonLoader = () => {
  return (
    <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6'>
      <SkeletonCard />
      <SkeletonCard />
      <SkeletonCard className='hidden sm:block' />
      <SkeletonCard className='hidden sm:block' />
      <SkeletonCard className='hidden lg:block' />
      <SkeletonCard className='hidden lg:block' />
      <SkeletonCard className='hidden xl:block' />
      <SkeletonCard className='hidden xl:block' />
    </div>
  );
};

export default ExperiencesSkeletonLoader;
