import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './components/pages/homePage/HomePage';
import 'tailwindcss';
import ExperiencesPage from './components/pages/experiencesPage/ExperiencesPage';
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
            </Routes>
          </div>
          <Footer />
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
