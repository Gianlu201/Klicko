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
import NotFoundPage from './pages/NotFoundPage';
import AboutPage from './pages/AboutPage';
import OrderConfirmationPage from './pages/OrderConfirmationPage';
import CouponPage from './pages/CouponPage';
import VouchersPage from './pages/VouchersPage';
import TermsAndConditionsPage from './pages/TermsAndConditionsPage';
import PrivacyPolicyPage from './pages/PrivacyPolicyPage';
import ContactPage from './pages/ContactPage';
import ScrollToTop from './components/ScrollToTop';
import LoyaltyPage from './pages/LoyaltyPage';
import FAQPage from './pages/FAQPage';

function App() {
  return (
    <>
      <BrowserRouter>
        <ScrollToTop />
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
              <Route path='/about' element={<AboutPage />} />
              <Route path='/login' element={<LoginPage />} />
              <Route path='/register' element={<RegisterPage />} />
              <Route path='/cart' element={<CartPage />} />
              <Route path='/dashboard' element={<DashboardPage />} />
              <Route path='/dashboard/:tab' element={<DashboardPage />} />
              <Route
                path='/dashboard/:tab/:option'
                element={<DashboardPage />}
              />
              <Route
                path='/dashboard/:tab/:option/:expId'
                element={<DashboardPage />}
              />
              <Route path='checkout' element={<CheckOutPage />} />
              <Route
                path='/orderConfirmation/:orderId'
                element={<OrderConfirmationPage />}
              />
              <Route path='/coupons' element={<CouponPage />} />
              <Route path='/redeemVoucher' element={<VouchersPage />} />
              <Route path='/loyalty' element={<LoyaltyPage />} />
              <Route
                path='/termsAndConditions'
                element={<TermsAndConditionsPage />}
              />
              <Route path='/privacyPolicy' element={<PrivacyPolicyPage />} />
              <Route path='/contact' element={<ContactPage />} />
              <Route path='/faq' element={<FAQPage />} />

              <Route path='*' element={<NotFoundPage />} />
            </Routes>
          </div>
          <Footer />
          <Toaster richColors />
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;
