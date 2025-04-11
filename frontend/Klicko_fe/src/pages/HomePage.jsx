import React from 'react';
import HeroComponent from '../components/homePage/HeroComponent';
import HighlightedComponent from '../components/homePage/highlightedComponent';
import CategoriesComponent from '../components/homePage/CategoriesComponent';
import PopularComponent from '../components/homePage/PopularComponent';
import WhyUsComponent from '../components/homePage/WhyUsComponent';
import CatComponent from '../components/homePage/CatComponent';

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
