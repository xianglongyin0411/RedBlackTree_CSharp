```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5487/22H2/2022Update)
11th Gen Intel Core i5-1145G7 2.60GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.201
  [Host]     : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method     | Mean     | Error   | StdDev  |
|----------- |---------:|--------:|--------:|
| InsertTest | 411.4 μs | 3.60 μs | 3.00 μs |
| DeleteTest | 722.1 μs | 2.38 μs | 1.98 μs |
| SearchTest | 801.0 μs | 4.25 μs | 3.76 μs |
