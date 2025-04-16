import './App.css';
import 'tailwindcss';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import ExperiencesPage from './pages/ExperiencesPage';
import DetailPage from './pages/DetailPage';
import CategoriesPage from './pages/CategoriesPage';
import Footer from './components/Footer';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import CartPage from './pages/CartPage';
import { Toaster } from 'sonner';
import DashboardPage from './pages/DashboardPage';
import CheckOutPage from './pages/CheckOutPage';

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
              <Route path='/cart' element={<CartPage />} />
              <Route path='/dashboard' element={<DashboardPage />} />
              <Route path='/dashboard/:tab' element={<DashboardPage />} />
              <Route
                path='/dashboard/:tab/:option'
                element={<DashboardPage />}
              />
              <Route path='checkout' element={<CheckOutPage />} />
            </Routes>
          </div>
          <Footer />
          <Toaster />
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
