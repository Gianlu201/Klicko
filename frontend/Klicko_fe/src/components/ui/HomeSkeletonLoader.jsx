import React from 'react';
import SkeletonCard from './SkeletonCard';

const HomeSkeletonLoader = () => {
  return (
    <div className='grid grid-cols-1 gap-8 mt-10 md:grid-cols-2 lg:grid-cols-3'>
      <SkeletonCard />
      <SkeletonCard />
      <SkeletonCard className='hidden lg:block' />
    </div>
  );
};

export default HomeSkeletonLoader;
