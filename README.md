## Summary

Benchmarks performance impact of https://github.com/StackExchange/Dapper/pull/1167

This benchmark runs mapping from `Id`, `Id`, ... columns to a tuple using `SqlMapper.Query` method with `map` argument.

## Result

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  DefaultJob : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT


```
|            Method | Rows |     Mean |    Error |   StdDev |
|------------------ |----- |---------:|---------:|---------:|
|         QueryLong |  100 | 37.78 μs | 0.105 μs | 0.087 μs |
| BaselineMultiMap2 |  100 | 39.38 μs | 0.317 μs | 0.281 μs |
| BaselineMultiMap3 |  100 | 39.57 μs | 0.152 μs | 0.135 μs |
| BaselineMultiMap5 |  100 | 40.75 μs | 0.127 μs | 0.112 μs |
| BaselineMultiMap7 |  100 | 42.80 μs | 0.077 μs | 0.060 μs |
|    FixedMultiMap2 |  100 | 38.76 μs | 0.150 μs | 0.125 μs |
|    FixedMultiMap3 |  100 | 40.46 μs | 0.164 μs | 0.128 μs |
|    FixedMultiMap5 |  100 | 42.16 μs | 0.144 μs | 0.128 μs |
|    FixedMultiMap7 |  100 | 44.60 μs | 0.320 μs | 0.284 μs |

| Columns | Overhead |
| ------- | -------- |
|       2 |  -1.57 % |
|       3 |   2.25 % |
|       5 |   3.46 % |
|       7 |   4.21 % |
