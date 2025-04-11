import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import ExperiencesPage from './pages/ExperiencesPage';
import DetailPage from './pages/DetailPage';
import CategoriesPage from './pages/CategoriesPage';
import 'tailwindcss';
import Footer from './components/Footer';

function App() {
  return (
    <>
      <BrowserRouter>
        <div className='min-h-screen flex flex-col'>
          <Navbar />
          <div className='grow'>
            <Routes>
              <Route path='/' element={<HomePage />} />
              <Route path='/experiences' element={<ExperiencesPage />} />
              <Route
                path='/experiences/detail/:experienceId'
                element={<DetailPage />}
              />
              <Route path='/categories' element={<CategoriesPage />} />
            </Routes>
          </div>
          <Footer />
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
