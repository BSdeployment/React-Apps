import { useMemo } from "react";

function ChaptersStatistics({ chapters }) {

  const stats = useMemo(() => {
    if (!chapters || chapters.length === 0) {
      return {
        totalReviews: 0,
        completedRounds: 0,
        nextRoundPercent: 0
      };
    }

    const totalReviews = chapters.reduce(
      (sum, c) => sum + (c.timesStudied || 0),
      0
    );

    const minTimes = Math.min(...chapters.map(c => c.timesStudied || 0));

    const nextRoundCount = chapters.filter(
      c => (c.timesStudied || 0) >= minTimes + 1
    ).length;

    const nextRoundPercent = Math.floor(
      (nextRoundCount / chapters.length) * 100
    );

    return {
      totalReviews,
      completedRounds: minTimes,
      nextRoundPercent
    };
  }, [chapters]);

  return (
    <div dir="rtl" className="col-12 col-md-4 mx-auto mt-4 card">
      <div className="card shadow-sm border-0 rounded-4">
        <div className="card-body">

          <h5 className="card-title text-center mb-4">
            סטטיסטיקה כללית
          </h5>

          <div className="mb-3 text-center">
            <h6 className="text-muted">סך כל החזרות</h6>
            <h3 className="fw-bold text-primary">
              {stats.totalReviews}
            </h3>
          </div>

          <div className="mb-3 text-center">
            <h6 className="text-muted">סבבים שהושלמו</h6>
            <h3 className="fw-bold text-success">
              {stats.completedRounds}
            </h3>
          </div>

          <div>
            <h6 className="text-muted text-center mb-2">
              התקדמות לסבב הבא
            </h6>

            <div className="progress" style={{ height: "20px" }}>
              <div
                className="progress-bar progress-bar-striped progress-bar-animated bg-info"
                role="progressbar"
                style={{ width: `${stats.nextRoundPercent}%` }}
              >
                {stats.nextRoundPercent}%
              </div>
            </div>
          </div>

        </div>
      </div>
    </div>
  );
}

export default ChaptersStatistics;
