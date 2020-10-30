package hr.darwin.sql;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

import javax.sql.DataSource;

public final class DataSourceSingleton {
    private static final String SERVER_NAME = "localhost";
    private static final int PORT = 1443;
    private static final String DATABASE_NAME = "FlotaVozilaDB";
    private static final String USER = "sa";
    private static final String PASSWORD = "";

    private DataSourceSingleton() {}

    private static DataSource instance;

    public static DataSource getInstance() {
        if (instance == null)  {
            instance = createInstance();
        }
        return instance;
    }

    private static DataSource createInstance() {
        SQLServerDataSource dataSource = new SQLServerDataSource();
        dataSource.setServerName(SERVER_NAME);
        dataSource.setPortNumber(PORT);
        dataSource.setDatabaseName(DATABASE_NAME);
        dataSource.setUser(USER);
        dataSource.setPassword(PASSWORD);
        return dataSource;
    }
}
