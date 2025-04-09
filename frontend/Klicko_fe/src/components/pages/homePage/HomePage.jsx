import React from 'react';
import HeroComponent from './HeroComponent';
import HighlightedComponent from './highlightedComponent';
import CategoriesComponent from './CategoriesComponent';
import PopularComponent from './PopularComponent';
import WhyUsComponent from './WhyUsComponent';
import CatComponent from './CatComponent';

const HomePage = () => {
  return (
    <div className='mb-20'>
      <HeroComponent />
      <HighlightedComponent />
      <CategoriesComponent />
      <PopularComponent />
      <WhyUsComponent />
      <CatComponent />
    </div>
  );
};

export default HomePage;
