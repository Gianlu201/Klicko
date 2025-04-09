import React from 'react';
import HeroComponent from './HeroComponent';
import HighlightedComponent from './highlightedComponent';
import CategoriesComponent from './CategoriesComponent';
import PopularComponent from './PopularComponent';
import WhyUsComponent from './WhyUsComponent';
import CatComponent from './CatComponent';
import Footer from '../../Footer';

const HomePage = () => {
  return (
    <>
      <HeroComponent />
      <HighlightedComponent />
      <CategoriesComponent />
      <PopularComponent />
      <WhyUsComponent />
      <CatComponent />
      <Footer />
    </>
  );
};

export default HomePage;
