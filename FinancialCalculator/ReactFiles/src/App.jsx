import Calculator from './components/Calculator.jsx';
import About from './components/About.jsx';
function App() {
  return (
    <div className="container py-5" dir="rtl" >
      <div style={{minHeight:"30px"}} className='bg-primary mb-3'>

      </div>
      <div className="row justify-content-center">
        <div className="col-12 col-lg-8">
          <h1 className="text-center mb-4 text-primary">מחשבון פקדון חודשי</h1>
          <Calculator />
          <About/>
        </div>
      </div>
    </div>
  );
}

export default App;
