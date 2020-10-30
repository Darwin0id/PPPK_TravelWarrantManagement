package hr.darwin.dal;

import hr.darwin.sql.SqlRepo;

public class RepoFactory {
    private RepoFactory() {

    }

    public static IRepo getRepository() throws Exception {
        return new SqlRepo();
    }
}
