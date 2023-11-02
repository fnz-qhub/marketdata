namespace MarketData.Db.Mongo.TestData;

using EphemeralMongo;

/// <summary>
/// Ephemeral Mongo's recommended static runner provider.
/// 
/// See https://gist.githubusercontent.com/asimmon/612b2d54f1a0d2b4e1115590d456e0be/raw/8475ef0926969bbe3d870fea0050a0bb22a598bd/MongoRunnerProvider.cs.
/// </summary>
public static class MongoRunnerProvider
{
    private static readonly object _lockObj = new();
    private static IMongoRunner? _runner;
    private static int _useCounter;

    public static IMongoRunner Get()
    {
        lock (_lockObj)
        {
            _runner ??= MongoRunner.Run(new MongoRunnerOptions
            {
                // Set shared static options

                // EXPERIMENTAL - Only works on Windows and modern .NET (netcoreapp3.1, net5.0, net6.0, net7.0 and so on):
                // Ensures that all MongoDB child processes are killed when the current process is prematurely killed,
                // for instance when killed from the task manager or the IDE unit tests window. Processes are managed as a unit using
                // job objects: https://learn.microsoft.com/en-us/windows/win32/procthread/job-objects
                KillMongoProcessesWhenCurrentProcessExits = true // Default: false
            });
            _useCounter++;
            return new MongoRunnerWrapper(_runner);
        }
    }

    private sealed class MongoRunnerWrapper : IMongoRunner
    {
        private IMongoRunner? _underlyingMongoRunner;

        public MongoRunnerWrapper(IMongoRunner underlyingMongoRunner) => _underlyingMongoRunner = underlyingMongoRunner;

        public string ConnectionString
            => _underlyingMongoRunner?.ConnectionString
            ?? throw new ObjectDisposedException(nameof(IMongoRunner));

        public void Import(string database, string collection, string inputFilePath, string? additionalArguments = null, bool drop = false)
        {
            if (_underlyingMongoRunner == null)
            {
                throw new ObjectDisposedException(nameof(IMongoRunner));
            }

            _underlyingMongoRunner.Import(database, collection, inputFilePath, additionalArguments, drop);
        }

        public void Export(string database, string collection, string outputFilePath, string? additionalArguments = null)
        {
            if (_underlyingMongoRunner == null)
            {
                throw new ObjectDisposedException(nameof(IMongoRunner));
            }

            _underlyingMongoRunner.Export(database, collection, outputFilePath, additionalArguments);
        }

        public void Dispose()
        {
            if (_underlyingMongoRunner != null)
            {
                _underlyingMongoRunner = null;
                StaticDispose();
            }
        }

        private static void StaticDispose()
        {
            lock (_lockObj)
            {
                if (_runner != null)
                {
                    _useCounter--;
                    if (_useCounter == 0)
                    {
                        _runner.Dispose();
                        _runner = null;
                    }
                }
            }
        }
    }
}