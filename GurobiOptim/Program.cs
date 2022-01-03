using System;
using Gurobi;

namespace GurobiOptim
{
    class Program
    {
        static void Main(string[] args)
        {
            MaximizeFunc();
        }

        static void MaximizeFunc()
        {
            try
            {
                Console.WriteLine();

                // Create an empty environment, set options and start
                GRBEnv env = new GRBEnv(true);
                env.Set("LogFile", "mip1.log");
                env.Start();

                // Create empty model
                GRBModel model = new GRBModel(env);

                // Create variables
                GRBVar x = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "x");
                GRBVar y = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "y");
                GRBVar z = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, "z");

                // Set objective: maximize x + y + 2z
                model.SetObjective(x + y + 2 * z, GRB.MAXIMIZE);

                // Add constraint: x + 2y + 3z <= 4
                model.AddConstr(x + 2 * y + 3 * z <= 4.0, "c0");

                // Add constraint: x + y >= 1
                model.AddConstr(x + y >= 1.0, "c1");

                // Optimize model
                model.Optimize();

                // Print outputs
                Console.WriteLine($"{x.VarName} {x.X}");
                Console.WriteLine($"{y.VarName} {y.X}");
                Console.WriteLine($"{z.VarName} {z.X}");

                Console.WriteLine();

                Console.WriteLine("Obj: " + model.ObjVal);

                // Dispose of model and env
                model.Dispose();
                env.Dispose();

                Console.ReadKey();

            }
            catch (GRBException e)
            {
                Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
            }
        }
    }
}
