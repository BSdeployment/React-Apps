import { useState } from 'react';

const initialResults = {
  totalPrincipal: null,
  totalInterest: null,
  finalAmount: null,
};

function Calculator() {
  const [monthlyDeposit, setMonthlyDeposit] = useState('');
  const [annualRate, setAnnualRate] = useState('');
  const [years, setYears] = useState('');
  const [results, setResults] = useState(initialResults);
  const [error, setError] = useState('');

  const handleCalculate = (event) => {
    event.preventDefault();
    setError('');

    const depositValue = Number(monthlyDeposit);
    const rateValue = Number(annualRate);
    const yearsValue = Number(years);

    if (
      monthlyDeposit === '' ||
      annualRate === '' ||
      years === '' ||
      Number.isNaN(depositValue) ||
      Number.isNaN(rateValue) ||
      Number.isNaN(yearsValue)
    ) {
      setError('יש למלא את כל השדות בערכים מספריים.');
      setResults(initialResults);
      return;
    }

    if (depositValue < 0 || rateValue < 0 || yearsValue <= 0) {
      setError('ערכים חייבים להיות חיוביים, ושנות חיסכון חייבות להיות גדול מאפס.');
      setResults(initialResults);
      return;
    }

    const months = yearsValue * 12;
    const monthlyRate = rateValue / 100 / 12;
    const totalPrincipal = depositValue * months;

    let finalAmount = 0;
    if (monthlyRate === 0) {
      finalAmount = totalPrincipal;
    } else {
      finalAmount =
        depositValue * ((Math.pow(1 + monthlyRate, months) - 1) / monthlyRate);
    }

    const totalInterest = finalAmount - totalPrincipal;

    setResults({
      totalPrincipal,
      totalInterest,
      finalAmount,
    });
  };

  const formatCurrency = (value) =>
    value !== null ? Number(value).toFixed(2) : '';

  return (
    <div>
      <form onSubmit={handleCalculate} className="mb-4">
        <div className="row g-3">
          <div className="col-12 col-md-4">
            <label htmlFor="monthlyDeposit" className="form-label">
              סכום הפקדה חודשית
            </label>
            <input
              id="monthlyDeposit"
              type="number"
              className="form-control"
              value={monthlyDeposit}
              onChange={(event) => setMonthlyDeposit(event.target.value)}
              min="0"
              step="0.01"
              placeholder="לדוגמה 500"
            />
          </div>
          <div className="col-12 col-md-4">
            <label htmlFor="annualRate" className="form-label">
              ריבית שנתית באחוזים
            </label>
            <input
              id="annualRate"
              type="number"
              className="form-control"
              value={annualRate}
              onChange={(event) => setAnnualRate(event.target.value)}
              min="0"
              step="0.01"
              placeholder="לדוגמה 4.5"
            />
          </div>
          <div className="col-12 col-md-4">
            <label htmlFor="years" className="form-label">
              מספר שנות חיסכון
            </label>
            <input
              id="years"
              type="number"
              className="form-control"
              value={years}
              onChange={(event) => setYears(event.target.value)}
              min="1"
              step="1"
              placeholder="לדוגמה 10"
            />
          </div>
        </div>
        {error && <div className="text-danger mt-3">{error}</div>}
        <div className="mt-4 text-center">
          <button type="submit" className="btn btn-primary">
            חשב
          </button>
        </div>
      </form>

      {results.finalAmount !== null && (
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">תוצאות החישוב</h5>
            <div className="row">
              <div className="col-12 col-md-4 mb-3 mb-md-0">
                <p className="mb-1">סך הקרן</p>
                <strong>{formatCurrency(results.totalPrincipal)} ₪</strong>
              </div>
              <div className="col-12 col-md-4 mb-3 mb-md-0">
                <p className="mb-1">סך הריבית</p>
                <strong>{formatCurrency(results.totalInterest)} ₪</strong>
              </div>
              <div className="col-12 col-md-4">
                <p className="mb-1">סכום מצטבר</p>
                <strong>{formatCurrency(results.finalAmount)} ₪</strong>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default Calculator;
