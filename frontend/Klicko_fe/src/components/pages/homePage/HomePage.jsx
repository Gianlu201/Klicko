import React from 'react';
import HeroComponent from './HeroComponent';
import HighlightedComponent from './highlightedComponent';
import CategoriesComponent from './CategoriesComponent';

const HomePage = () => {
  return (
    <div className='mb-20'>
      <HeroComponent />
      <HighlightedComponent />
      <CategoriesComponent />
    </div>
  );
};

export default HomePage;
