import React from 'react';
import HeroComponent from './HeroComponent';
import HighlightedComponent from './highlightedComponent';
import CategoriesComponent from './CategoriesComponent';
import PopularComponent from './PopularComponent';
import WhyUsComponent from './WhyUsComponent';
import CatComponent from './CatComponent';

const HomePage = () => {
  return (
    <>
      <HeroComponent />
      <HighlightedComponent />
      <CategoriesComponent />
      <PopularComponent />
      <WhyUsComponent />
      <CatComponent />
    </>
  );
};

export default HomePage;
