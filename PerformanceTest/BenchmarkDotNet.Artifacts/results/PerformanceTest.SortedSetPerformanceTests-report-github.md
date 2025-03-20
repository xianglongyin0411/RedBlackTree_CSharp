```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5487/22H2/2022Update)
11th Gen Intel Core i5-1145G7 2.60GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.201
  [Host]     : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method     | Mean       | Error    | StdDev   | Median     |
|----------- |-----------:|---------:|---------:|-----------:|
| InsertTest |   412.0 μs |  7.29 μs | 18.69 μs |   404.9 μs |
| DeleteTest | 1,275.5 μs | 30.59 μs | 86.27 μs | 1,251.1 μs |
| SearchTest |   800.7 μs | 14.97 μs | 15.38 μs |   795.9 μs |
