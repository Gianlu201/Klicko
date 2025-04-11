import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import ExperiencesPage from './pages/ExperiencesPage';
import DetailPage from './pages/DetailPage';
import CategoriesPage from './pages/CategoriesPage';
import 'tailwindcss';
import Footer from './components/Footer';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';

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
              <Route path='/login' element={<LoginPage />} />
              <Route path='/register' element={<RegisterPage />} />
            </Routes>
          </div>
          <Footer />
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
