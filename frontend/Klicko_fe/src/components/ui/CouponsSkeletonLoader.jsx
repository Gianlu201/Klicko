import React from 'react';
import SkeletonCard from './SkeletonCard';

const CouponsSkeletonLoader = () => {
  return (
    <div className='grid grid-cols-1 gap-6 mb-6 md:grid-cols-2 lg:grid-cols-3'>
      <SkeletonCard />
      <SkeletonCard />
      <SkeletonCard className='hidden md:block' />
      <SkeletonCard className='hidden md:block' />
      <SkeletonCard className='hidden lg:block' />
      <SkeletonCard className='hidden lg:block' />
    </div>
  );
};

export default CouponsSkeletonLoader;
