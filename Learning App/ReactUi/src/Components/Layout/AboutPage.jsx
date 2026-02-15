


function AboutPage() {

    return (
        <div>
            <main class="flex-fill d-flex align-items-center">
                <div class="container text-center py-5">
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-10 col-lg-8">
                            <h1 class="display-5 fw-bold mb-4">Developed by BSDotNet</h1>
                            <p class="lead text-muted mb-3">
                                Mobile, Desktop, Web, Cybersecurity, and Software Analysis Development.
                            </p>
                            <p class="mb-4">
                                This application is designed to assist students with learning and revision.
                                It is fully open-source, and all resources are available in the repository.
                                Contributions are welcome for anyone who wishes to support the project.
                            </p>
                            <div class="d-grid gap-2 d-sm-flex justify-content-sm-center">
                                <a href="https://github.com/BSdeployment/" target="_blank" class="btn btn-primary btn-lg px-4">
                                    Visit GitHub Repository
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
    )
}

export default AboutPage;